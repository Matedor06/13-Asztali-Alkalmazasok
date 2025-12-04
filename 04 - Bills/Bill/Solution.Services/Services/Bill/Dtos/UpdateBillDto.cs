namespace Solution.Services.Services.Bill.Dtos;

public class UpdateBillDto
{
    public string BillNumber { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    public List<UpdateBillItemDto> BillItems { get; set; } = new();
}

public class UpdateBillItemDto
{
    public int? Id { get; set; } // Nullable, mert új tételnek nincs ID-ja
    public string Name { get; set; } = string.Empty;
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
}
