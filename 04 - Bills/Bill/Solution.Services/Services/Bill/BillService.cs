namespace Solution.Services.Services.Bill;

public class BillService : IBillService
{
    private readonly AppDbContext _context;

    public BillService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<BillModel>> CreateAsync(BillModel model)
    {
        bool exists = await _context.Bills.AnyAsync(x => x.BillNumber.ToLower() == model.BillNumber.ToLower().Trim());

        if (exists)
        {
            return Error.Conflict(description: "Bill with this number already exists!");
        }

        var bill = model.ToEntity();
        
        await _context.Bills.AddAsync(bill);
        await _context.SaveChangesAsync();

        return new BillModel(bill);
    }

    public async Task<ErrorOr<BillModel>> UpdateAsync(BillModel model)
    {
        var entity = await _context.Bills
            .Include(b => b.BillItems)
            .FirstOrDefaultAsync(b => b.Id == model.Id);

        if (entity == null)
            return Error.NotFound(description: $"Bill with ID {model.Id} not found");

        entity.BillNumber = model.BillNumber;
        entity.DateIssued = model.DateIssued;
        
        // Remove old items
        _context.BillItems.RemoveRange(entity.BillItems);

        // Add new items
        entity.BillItems = model.BillItems.Select(item => new BillItemEntity
        {
            Name = item.Name,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            BillId = model.Id
        }).ToList();

        await _context.SaveChangesAsync();

        return new BillModel(entity);
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id)
    {
        var result = await _context.Bills
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0 ? true : Error.NotFound(description: "Bill not found");
    }

    public async Task<ErrorOr<BillModel>> GetByIdAsync(int id)
    {
        var bill = await _context.Bills
            .Include(b => b.BillItems)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (bill is null)
        {
            return Error.NotFound(description: "Bill not found.");
        }

        return new BillModel(bill);
    }

    public async Task<ErrorOr<List<BillModel>>> GetAllAsync() =>
        await _context.Bills.AsNoTracking()
                           .Include(b => b.BillItems)
                           .Select(x => new BillModel(x))
                           .ToListAsync();
}
