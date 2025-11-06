using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Services.Services.Bill
{
    public interface IBillItemService
    {
        Task<ErrorOr<BillItemModel>> CreateAsync(BillItemModel model);
        Task<ErrorOr<Success>> UpdateAsync(BillItemModel model);
        Task<ErrorOr<Success>> DeleteAsync(int billItemId);
        Task<ErrorOr<BillItemModel>> GetByIdAsync(int billItemId);
        Task<ErrorOr<List<BillItemModel>>> GetAllAsync();
        Task<ErrorOr<PaginationModel<BillItemModel>>> GetPagedAsync(int page = 0);
    }
}