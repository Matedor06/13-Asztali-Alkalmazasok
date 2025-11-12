using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<ErrorOr<BillModel>> CreateAsync(BillModel model)
    {
        try
        {
            var entity = model.ToEntity();
            _context.Bills.Add(entity);
            await _context.SaveChangesAsync();

            return new BillModel(entity);
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillModel>> UpdateAsync(BillModel model)
    {
        try
        {
            var entity = await _context.Bills
                .Include(b => b.BillItems)
                .FirstOrDefaultAsync(b => b.Id == model.Id);

            if (entity == null)
                return Error.NotFound(description: $"Bill with ID {model.Id} not found");

            entity.BillNumber = model.BillNumber;
            entity.DateIssued = model.DateIssued;
            
            // Update BillItems
            _context.BillItems.RemoveRange(entity.BillItems);
            entity.BillItems = model.BillItems.Select(m => m.ToEntity()).ToList();

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
