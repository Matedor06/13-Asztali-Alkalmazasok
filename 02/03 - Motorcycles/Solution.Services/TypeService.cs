namespace Solution.Services;

public class TypeService(AppDbContext dbContext) : ITypeService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<TypeModel>> CreateAsync(TypeModel model)
    {
        bool exists = await dbContext.Types.AnyAsync(x => x.Name.ToLower() == model.Name.ToLower().Trim());

        if (exists)
        {
            return Error.Conflict(description: "Type already exists!");
        }

        var type = model.ToEntity();
        type.Id = 0; // Reset ID for new entity
        type.Name = model.Name.Trim();
        
        await dbContext.Types.AddAsync(type);
        await dbContext.SaveChangesAsync();

        return new TypeModel(type);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(TypeModel model)
    {
        var result = await dbContext.Types.AsNoTracking()
                                          .Where(x => x.Id == model.Id)
                                          .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name.Trim()));
        
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int typeId)
    {
        var result = await dbContext.Types.AsNoTracking()
                                          .Where(x => x.Id == typeId)
                                          .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<TypeModel>> GetByIdAsync(int typeId)
    {
        var type = await dbContext.Types.FirstOrDefaultAsync(x => x.Id == typeId);

        if (type is null)
        {
            return Error.NotFound(description: "Type not found.");
        }

        return new TypeModel(type);
    }

    public async Task<ErrorOr<List<TypeModel>>> GetAllAsync() =>
        await dbContext.Types.AsNoTracking()
                             .Select(x => new TypeModel(x))
                             .ToListAsync();

    public async Task<ErrorOr<PaginationModel<TypeModel>>> GetPagedAsync(int page = 0)
    {
        var totalCount = await dbContext.Types.CountAsync();
        var items = await dbContext.Types.AsNoTracking()
                                         .Skip(page * ROW_COUNT)
                                         .Take(ROW_COUNT)
                                         .Select(x => new TypeModel(x))
                                         .ToListAsync();

        return new PaginationModel<TypeModel>
        {
            Items = items,
            Count = totalCount
        };
    }
}