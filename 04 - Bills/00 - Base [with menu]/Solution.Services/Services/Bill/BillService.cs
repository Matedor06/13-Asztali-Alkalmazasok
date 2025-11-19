using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solution.Services.Services.Bill.Dtos;

namespace Solution.Services.Services.Bill;

public class BillService : IBillService
{
    private readonly AppDbContext _context;

    public BillService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<BillModel>>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Bills
                .Include(b => b.BillItems)
                .ToListAsync();

            var models = entities.Select(e => new BillModel(e)).ToList();
            return models;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillModel>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Bills
                .Include(b => b.BillItems)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (entity == null)
                return Error.NotFound(description: $"Bill with ID {id} not found");

            return new BillModel(entity);
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillModel>> CreateAsync(CreateBillDto dto)
    {
        try
        {
            var entity = new BillEntity
            {
                BillNumber = dto.BillNumber,
                DateIssued = dto.DateIssued,
                BillItems = dto.BillItems.Select(item => new BillItemEntity
                {
                    Name = item.Name,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                }).ToList()
            };

            _context.Bills.Add(entity);
            await _context.SaveChangesAsync();

            return new BillModel(entity);
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillModel>> UpdateAsync(int id, UpdateBillDto dto)
    {
        try
        {
            var entity = await _context.Bills
                .Include(b => b.BillItems)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (entity == null)
                return Error.NotFound(description: $"Bill with ID {id} not found");

            entity.BillNumber = dto.BillNumber;
            entity.DateIssued = dto.DateIssued;
            
            // Töröljük a régi tételeket
            _context.BillItems.RemoveRange(entity.BillItems);

            // Hozzáadjuk az új tételeket
            entity.BillItems = dto.BillItems.Select(item => new BillItemEntity
            {
                Id = item.Id ?? 0, // Ha van ID használjuk, különben 0 (új tétel)
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                BillId = id
            }).ToList();

            await _context.SaveChangesAsync();

            return new BillModel(entity);
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Bills.FindAsync(id);

            if (entity == null)
                return Error.NotFound(description: $"Bill with ID {id} not found");

            _context.Bills.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }
}
