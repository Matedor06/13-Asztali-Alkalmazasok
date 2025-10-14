namespace Solution.Services;

public class CarService(AppDbContext dbContext) : ICarService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<CarModel>> CreateAsync(CarModel model)
    {
        bool exists = await dbContext.Cars.AnyAsync(x => x.ManufacturerId == model.Manufacturer.Id &&
                                                        x.Model.ToLower() == model.Model.ToLower().Trim() &&
                                                        x.ReleaseYear == model.ReleaseYear.Value);

        if (exists)
        {
            return Error.Conflict(description: "Car already exists!");
        }

        var car = model.ToEntity();
        car.PublicId = Guid.NewGuid().ToString();
        
        await dbContext.Cars.AddAsync(car);
        await dbContext.SaveChangesAsync();

        return new CarModel(car)
        {
            Manufacturer = model.Manufacturer
        };
    }

    public async Task<ErrorOr<Success>> UpdateAsync(CarModel model)
    {
        var result = await dbContext.Cars.AsNoTracking()
                                         .Where(x => x.PublicId == model.Id)
                                         .ExecuteUpdateAsync(x => x.SetProperty(p => p.PublicId, model.Id)
                                                                   .SetProperty(p => p.ManufacturerId, model.Manufacturer.Id)
                                                                   .SetProperty(p => p.TypeId, model.Type.Id)
                                                                   .SetProperty(p => p.Model, model.Model)
                                                                   .SetProperty(p => p.Cubic, model.Cubic.Value)
                                                                   .SetProperty(p => p.ReleaseYear, model.ReleaseYear.Value)
                                                                   .SetProperty(p => p.NumberOfDoors, model.NumberOfDoors.Value)
                                                                   .SetProperty(p => p.ImageId, model.ImageId)
                                                                   .SetProperty(p => p.WebContentLink, model.WebContentLink));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(string carId)
    {
        var result = await dbContext.Cars.AsNoTracking()
                                         .Include(x => x.Manufacturer)
                                         .Where(x => x.PublicId == carId)
                                         .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<CarModel>> GetByIdAsync(string carId)
    {
        var car = await dbContext.Cars.Include(x => x.Manufacturer)
                                      .FirstOrDefaultAsync(x => x.PublicId == carId);

        if (car is null)
        {
            return Error.NotFound(description: "Car not found.");
        }

        return new CarModel(car);
    }

    public async Task<ErrorOr<List<CarModel>>> GetAllAsync() =>
        await dbContext.Cars.AsNoTracking()
                            .Include(x => x.Manufacturer)
                            .Select(x => new CarModel(x))
                            .ToListAsync();

    public async Task<ErrorOr<PaginationModel<CarModel>>> GetPagedAsync(int page = 0)
    {
        page = page <= 0 ? 1 : page - 1;

        var cars = await dbContext.Cars.AsNoTracking()
                                       .Include(x => x.Manufacturer)
                                       .Include(x => x.Type)
                                       .Skip(page * ROW_COUNT)
                                       .Take(ROW_COUNT)
                                       .Select(x => new CarModel(x))
                                       .ToListAsync();

        var paginationModel = new PaginationModel<CarModel>
        {
            Items = cars,
            Count = await dbContext.Cars.CountAsync()
        };

        return paginationModel;
    }
}