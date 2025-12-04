using Solution.Services.Services.Bill.Dtos;
using Solution.Services.Services.Bill.Models;
using Solution.Services.Services.BillItem.Models;

namespace Solution.Services.Extensions;

public static class BillModelExtensions
{
    public static CreateBillDto ToCreateDto(this BillModel model)
    {
        return new CreateBillDto
        {
            BillNumber = model.BillNumber,
            DateIssued = model.DateIssued,
            BillItems = model.BillItems.Select(item => new CreateBillItemDto
            {
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList()
        };
    }

    public static UpdateBillDto ToUpdateDto(this BillModel model)
    {
        return new UpdateBillDto
        {
            BillNumber = model.BillNumber,
            DateIssued = model.DateIssued,
            BillItems = model.BillItems.Select(item => new UpdateBillItemDto
            {
                Id = item.Id > 0 ? item.Id : null,
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList()
        };
    }
}
