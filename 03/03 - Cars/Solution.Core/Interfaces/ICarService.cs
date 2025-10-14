namespace Solution.Core.Interfaces;

public interface ICarService
{
    Task<ErrorOr<CarModel>> CreateAsync(CarModel model);
    Task<ErrorOr<Success>> UpdateAsync(CarModel model);
    Task<ErrorOr<Success>> DeleteAsync(string carId);
    Task<ErrorOr<CarModel>> GetByIdAsync(string carId);
    Task<ErrorOr<List<CarModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<CarModel>>> GetPagedAsync(int page = 0);
}