namespace Solution.Services.Services.BillItem;

public class BillItemService : IBillItemService
{
    private readonly AppDbContext _context;

    public BillItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<BillItemModel>>> GetAllAsync()
    {
        try
        {
            var entities = await _context.BillItems.ToListAsync();
            var models = entities.Select(e => new BillItemModel(e)).ToList();
            return models;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillItemModel>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.BillItems.FindAsync(id);

            if (entity == null)
                return Error.NotFound(description: $"BillItem with ID {id} not found");

            return new BillItemModel(entity);
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<List<BillItemModel>>> GetByBillIdAsync(int billId)
    {
        try
        {
            var entities = await _context.BillItems
                .Where(bi => bi.BillId == billId)
                .ToListAsync();

            var models = entities.Select(e => new BillItemModel(e)).ToList();
            return models;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillItemModel>> CreateAsync(BillItemModel model)
    {
        try
        {
            var entity = model.ToEntity();
            _context.BillItems.Add(entity);
            await _context.SaveChangesAsync();

            return new BillItemModel(entity);
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }

    public async Task<ErrorOr<BillItemModel>> UpdateAsync(BillItemModel model)
    {
        try
        {
            var entity = await _context.BillItems.FindAsync(model.Id);

            if (entity == null)
                return Error.NotFound(description: $"BillItem with ID {model.Id} not found");

            entity.Name = model.Name;
            entity.UnitPrice = model.UnitPrice;
            entity.Quantity = model.Quantity;
            entity.BillId = model.BillId;

            await _context.SaveChangesAsync();

            return new BillItemModel(entity);
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
            var entity = await _context.BillItems.FindAsync(id);

            if (entity == null)
                return Error.NotFound(description: $"BillItem with ID {id} not found");

            _context.BillItems.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: ex.Message);
        }
    }
}
