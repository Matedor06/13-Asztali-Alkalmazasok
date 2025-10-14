namespace Solution.Services;

public class ManufacturerService(AppDbContext dbContext) : IManufacturerService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<ManufacturerModel>> CreateAsync(ManufacturerModel model)
    {
        bool exists = await dbContext.Manufacturers.AnyAsync(x => x.Name.ToLower() == model.Name.ToLower().Trim());

        if (exists)
        {
            return Error.Conflict(description: "Manufacturer already exists!");
        }

        var manufacturer = model.ToEntity();
        manufacturer.Id = 0; // Reset ID for new entity
        manufacturer.Name = model.Name.Trim();
        
        await dbContext.Manufacturers.AddAsync(manufacturer);
        await dbContext.SaveChangesAsync();

        return new ManufacturerModel(manufacturer);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(ManufacturerModel model)
    {
        var result = await dbContext.Manufacturers.AsNoTracking()
                                          .Where(x => x.Id == model.Id)
                                          .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name.Trim()));
        
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int manufacturerId)
    {
        var result = await dbContext.Manufacturers.AsNoTracking()
                                          .Where(x => x.Id == manufacturerId)
                                          .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<ManufacturerModel>> GetByIdAsync(int manufacturerId)
    {
        var manufacturer = await dbContext.Manufacturers.FirstOrDefaultAsync(x => x.Id == manufacturerId);

        if (manufacturer is null)
        {
            return Error.NotFound(description: "Manufacturer not found.");
        }

        return new ManufacturerModel(manufacturer);
    }

    public async Task<ErrorOr<List<ManufacturerModel>>> GetAllAsync() =>
        await dbContext.Manufacturers.AsNoTracking()
                             .Select(x => new ManufacturerModel(x))
                             .ToListAsync();

    public async Task<ErrorOr<PaginationModel<ManufacturerModel>>> GetPagedAsync(int page = 0)
    {
        page = page <= 0 ? 1 : page - 1;


        var totalCount = await dbContext.Manufacturers.CountAsync();
        var items = await dbContext.Manufacturers.AsNoTracking()
                                         .Skip(page * ROW_COUNT)
                                         .Take(ROW_COUNT)
                                         .Select(x => new ManufacturerModel(x))
                                         .ToListAsync();

        return new PaginationModel<ManufacturerModel>
        {
            Items = items,
            Count = totalCount
        };
    }
}