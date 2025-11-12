using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Services.Services.Bill;

public interface IBillService
{
    Task<ErrorOr<List<BillModel>>> GetAllAsync();
    Task<ErrorOr<BillModel>> GetByIdAsync(int id);
    Task<ErrorOr<BillModel>> CreateAsync(BillModel model);
    Task<ErrorOr<BillModel>> UpdateAsync(BillModel model);
    Task<ErrorOr<bool>> DeleteAsync(int id);
}
