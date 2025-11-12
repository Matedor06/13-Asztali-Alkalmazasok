namespace Solution.Services.Services.BillItem.Models;

public partial class BillItemModel : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int unitPrice;

    [ObservableProperty]
    private int quantity;

    [ObservableProperty]
    private int billId;

    public int TotalPrice => UnitPrice * Quantity;

    public BillItemModel()
    {
    }

    public BillItemModel(BillItemEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        UnitPrice = entity.UnitPrice;
        Quantity = entity.Quantity;
        BillId = entity.BillId;
    }

    public BillItemEntity ToEntity()
    {
        return new BillItemEntity
        {
            Id = Id,
            Name = Name,
            UnitPrice = UnitPrice,
            Quantity = Quantity,
            BillId = BillId
        };
    }
}
