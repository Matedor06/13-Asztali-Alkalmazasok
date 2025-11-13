namespace Solution.Services.Services.Bill.Models;

public partial class BillModel : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string billNumber = string.Empty;

    [ObservableProperty]
    private DateTime dateIssued = DateTime.Now;

    [ObservableProperty]
    private ObservableCollection<BillItemModel> billItems = new();

    public BillModel()
    {
    }

    public BillModel(BillEntity entity)
    {
        Id = entity.Id;
        BillNumber = entity.BillNumber;
        DateIssued = entity.DateIssued;
        BillItems = new ObservableCollection<BillItemModel>(
            entity.BillItems?.Select(item => new BillItemModel(item)) ?? Enumerable.Empty<BillItemModel>()
        );
    }

    public BillEntity ToEntity()
    {
        return new BillEntity
        {
            Id = Id,
            BillNumber = BillNumber,
            DateIssued = DateIssued,
            BillItems = BillItems.Select(item => item.ToEntity()).ToList()
        };
    }
}