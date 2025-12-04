namespace Solution.Services.Services.BillItem;

public class BillItemService : IBillItemService
{
    private readonly AppDbContext _context;

    public BillItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<BillItemModel>> CreateAsync(BillItemModel model)
    {
        var billItem = model.ToEntity();
        
        await _context.BillItems.AddAsync(billItem);
        await _context.SaveChangesAsync();

        return new BillItemModel(billItem);
    }

    public async Task<ErrorOr<BillItemModel>> UpdateAsync(BillItemModel model)
    {
        var result = await _context.BillItems
            .Where(x => x.Id == model.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name)
                                      .SetProperty(p => p.UnitPrice, model.UnitPrice)
                                      .SetProperty(p => p.Quantity, model.Quantity)
                                      .SetProperty(p => p.BillId, model.BillId));

        if (result > 0)
        {
            var updatedItem = await _context.BillItems.FindAsync(model.Id);
            return new BillItemModel(updatedItem);
        }

        return Error.NotFound(description: "BillItem not found");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id)
    {
        var result = await _context.BillItems
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0 ? true : Error.NotFound(description: "BillItem not found");
    }

    public async Task<ErrorOr<BillItemModel>> GetByIdAsync(int id)
    {
        var billItem = await _context.BillItems.FirstOrDefaultAsync(x => x.Id == id);

        if (billItem is null)
        {
            return Error.NotFound(description: "BillItem not found.");
        }

        return new BillItemModel(billItem);
    }

    public async Task<ErrorOr<List<BillItemModel>>> GetAllAsync() =>
        await _context.BillItems.AsNoTracking()
                               .Select(x => new BillItemModel(x))
                               .ToListAsync();

    public async Task<ErrorOr<List<BillItemModel>>> GetByBillIdAsync(int billId) =>
        await _context.BillItems.AsNoTracking()
                               .Where(bi => bi.BillId == billId)
                               .Select(x => new BillItemModel(x))
                               .ToListAsync();
}
