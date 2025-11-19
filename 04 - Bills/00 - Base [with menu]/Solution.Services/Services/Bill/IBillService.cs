using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solution.Services.Services.Bill.Dtos;

namespace Solution.Services.Services.Bill;

public interface IBillService
{
    Task<ErrorOr<List<BillModel>>> GetAllAsync();
    Task<ErrorOr<BillModel>> GetByIdAsync(int id);
    Task<ErrorOr<BillModel>> CreateAsync(CreateBillDto dto);
    Task<ErrorOr<BillModel>> UpdateAsync(int id, UpdateBillDto dto);
    Task<ErrorOr<bool>> DeleteAsync(int id);
}
