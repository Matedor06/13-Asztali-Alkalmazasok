namespace Solution.Services.Services.Bill.Dtos;

public class CreateBillDto
{
    public string BillNumber { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; } = DateTime.Now;
    public List<CreateBillItemDto> BillItems { get; set; } = new();
}

public class CreateBillItemDto
{
    public string Name { get; set; } = string.Empty;
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
}
