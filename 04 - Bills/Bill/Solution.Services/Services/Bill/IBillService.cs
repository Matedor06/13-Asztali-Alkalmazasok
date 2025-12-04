namespace Solution.Services.Services.Bill;

public interface IBillService
{
    Task<ErrorOr<BillModel>> CreateAsync(BillModel model);
    Task<ErrorOr<BillModel>> UpdateAsync(BillModel model);
    Task<ErrorOr<bool>> DeleteAsync(int id);
    Task<ErrorOr<BillModel>> GetByIdAsync(int id);
    Task<ErrorOr<List<BillModel>>> GetAllAsync();
}
