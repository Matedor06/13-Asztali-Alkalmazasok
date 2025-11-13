using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Services.Services.BillItem;

public interface IBillItemService
{
    Task<ErrorOr<List<BillItemModel>>> GetAllAsync();
    Task<ErrorOr<BillItemModel>> GetByIdAsync(int id);
    Task<ErrorOr<List<BillItemModel>>> GetByBillIdAsync(int billId);
    Task<ErrorOr<BillItemModel>> CreateAsync(BillItemModel model);
    Task<ErrorOr<BillItemModel>> UpdateAsync(BillItemModel model);
    Task<ErrorOr<bool>> DeleteAsync(int id);
}
