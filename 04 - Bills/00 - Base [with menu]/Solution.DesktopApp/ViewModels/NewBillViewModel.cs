namespace Solution.DesktopApp.ViewModels;

public partial class NewBillViewModel : ObservableObject
{
    public const string Name = nameof(NewBillViewModel);

    private readonly IBillService _billService;
    private readonly IBillItemService _billItemService;

    [ObservableProperty]
    private BillModel currentBill = new()
    {
        DateIssued = DateTime.Now,
        BillItems = new ObservableCollection<BillItemModel>()
    };

    [ObservableProperty]
    private BillItemModel currentBillItem = new();

    [ObservableProperty]
    private ObservableCollection<BillItemModel> billItems = new();

    [ObservableProperty]
    private bool isEditMode;

    [ObservableProperty]
    private bool isItemEditMode;

    [ObservableProperty]
    private int editingItemIndex = -1;

    [ObservableProperty]
    private bool canSaveBill;

    public NewBillViewModel(IBillService billService, IBillItemService billItemService)
    {
        _billService = billService;
        _billItemService = billItemService;

        BillItems.CollectionChanged += BillItems_CollectionChanged;
    }

    private void BillItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        CanSaveBill = BillItems.Count > 0;
    }

    [RelayCommand]
    private void AddOrUpdateBillItem()
    {
        if (string.IsNullOrWhiteSpace(CurrentBillItem.Name) || 
            CurrentBillItem.UnitPrice <= 0 || 
            CurrentBillItem.Quantity <= 0)
        {
            Application.Current.MainPage.DisplayAlert("Hiba", "Minden mezõ kitöltése kötelezõ! Az egységár és mennyiség nem lehet kisebb mint 1.", "OK");
            return;
        }

        if (IsItemEditMode && EditingItemIndex >= 0)
        {
            // Szerkesztés
            BillItems[EditingItemIndex] = new BillItemModel
            {
                Id = CurrentBillItem.Id,
                Name = CurrentBillItem.Name,
                UnitPrice = CurrentBillItem.UnitPrice,
                Quantity = CurrentBillItem.Quantity,
                BillId = CurrentBillItem.BillId
            };

            IsItemEditMode = false;
            EditingItemIndex = -1;
        }
        else
        {
            // Új tétel hozzáadása
            BillItems.Add(new BillItemModel
            {
                Name = CurrentBillItem.Name,
                UnitPrice = CurrentBillItem.UnitPrice,
                Quantity = CurrentBillItem.Quantity
            });
        }

        // Mezõk törlése
        CurrentBillItem = new BillItemModel();
    }

    [RelayCommand]
    private async Task DeleteBillItem(BillItemModel item)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Figyelmeztetés", 
            $"Biztosan törölni szeretné a '{item.Name}' tételt?", 
            "Igen", 
            "Nem");

        if (confirm)
        {
            BillItems.Remove(item);
        }
    }

    [RelayCommand]
    private void EditBillItem(BillItemModel item)
    {
        EditingItemIndex = BillItems.IndexOf(item);
        IsItemEditMode = true;

        CurrentBillItem = new BillItemModel
        {
            Id = item.Id,
            Name = item.Name,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            BillId = item.BillId
        };
    }

    [RelayCommand]
    private void CancelItemEdit()
    {
        IsItemEditMode = false;
        EditingItemIndex = -1;
        CurrentBillItem = new BillItemModel();
    }

    [RelayCommand]
    private async Task SaveBill()
    {
        if (string.IsNullOrWhiteSpace(CurrentBill.BillNumber))
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", "A számla száma kötelezõ!", "OK");
            return;
        }

        if (CurrentBill.DateIssued > DateTime.Now)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", "A számla kelte nem lehet késõbbi, mint a jelenlegi dátum!", "OK");
            return;
        }

        if (BillItems.Count == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", "Legalább egy tétel hozzáadása kötelezõ!", "OK");
            return;
        }

        // Frissítjük a CurrentBill BillItems-et a helyi BillItems gyûjteménybõl
        CurrentBill.BillItems = new ObservableCollection<BillItemModel>(BillItems);

        ErrorOr<BillModel> result;

        if (IsEditMode && CurrentBill.Id > 0)
        {
            // UPDATE - DTO konverzió használata
            var updateDto = CurrentBill.ToUpdateDto();
            result = await _billService.UpdateAsync(CurrentBill.Id, updateDto);
        }
        else
        {
            // CREATE - DTO konverzió használata
            var createDto = CurrentBill.ToCreateDto();
            result = await _billService.CreateAsync(createDto);
        }

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", result.Errors[0].Description, "OK");
            return;
        }

        await Application.Current.MainPage.DisplayAlert("Siker", 
            IsEditMode ? "Számla sikeresen módosítva!" : "Számla sikeresen elmentve!", 
            "OK");

        await ResetForm();
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task ResetForm()
    {
        CurrentBill = new BillModel
        {
            DateIssued = DateTime.Now,
            BillItems = new ObservableCollection<BillItemModel>()
        };

        BillItems.Clear();
        CurrentBillItem = new BillItemModel();
        IsEditMode = false;
        IsItemEditMode = false;
        EditingItemIndex = -1;
    }

    public void LoadBillForEdit(BillModel bill)
    {
        IsEditMode = true;
        CurrentBill = new BillModel
        {
            Id = bill.Id,
            BillNumber = bill.BillNumber,
            DateIssued = bill.DateIssued
        };

        BillItems.Clear();
        foreach (var item in bill.BillItems)
        {
            BillItems.Add(new BillItemModel
            {
                Id = item.Id,
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                BillId = item.BillId
            });
        }
    }
}
