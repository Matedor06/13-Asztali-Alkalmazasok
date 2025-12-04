namespace Solution.Services.Services.BillItem;

public interface IBillItemService
{
    Task<ErrorOr<BillItemModel>> CreateAsync(BillItemModel model);
    Task<ErrorOr<BillItemModel>> UpdateAsync(BillItemModel model);
    Task<ErrorOr<bool>> DeleteAsync(int id);
    Task<ErrorOr<BillItemModel>> GetByIdAsync(int id);
    Task<ErrorOr<List<BillItemModel>>> GetAllAsync();
    Task<ErrorOr<List<BillItemModel>>> GetByBillIdAsync(int billId);
}
