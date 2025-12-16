using FluentValidation;

namespace Solution.DesktopApp.ViewModels;

public partial class NewBillViewModel : ObservableObject
{
    public const string Name = nameof(NewBillViewModel);

    private readonly IBillService _billService;
    private readonly IBillItemService _billItemService;
    private readonly IValidator<BillModel> _billValidator;

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

    public NewBillViewModel(IBillService billService, IBillItemService billItemService, IValidator<BillModel> billValidator)
    {
        _billService = billService;
        _billItemService = billItemService;
        _billValidator = billValidator;
    }

    [RelayCommand]
    private void AddOrUpdateBillItem()
    {
        if (string.IsNullOrWhiteSpace(CurrentBillItem.Name) || 
            CurrentBillItem.UnitPrice <= 0 || 
            CurrentBillItem.Quantity <= 0)
        {
            Application.Current.MainPage.DisplayAlert("Hiba", "Minden mező kitöltése kötelező! Az egységár és mennyiség nem lehet kisebb mint 1.", "OK");
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

        // Mezők törlése
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
        // Frissítjük a CurrentBill BillItems-et a helyi BillItems gyűjteményből
        CurrentBill.BillItems = new ObservableCollection<BillItemModel>(BillItems);

        // FluentValidation használata
        var validationResult = await _billValidator.ValidateAsync(CurrentBill);
        
        if (!validationResult.IsValid)
        {
            // Hibaüzenetek összegyűjtése
            var errorMessages = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            await Application.Current.MainPage.DisplayAlert("Validációs hiba", errorMessages, "OK");
            return;
        }

        ErrorOr<BillModel> result;

        if (IsEditMode && CurrentBill.Id > 0)
        {
            // UPDATE - Model használata közvetlenül
            result = await _billService.UpdateAsync(CurrentBill);
        }
        else
        {
            // CREATE - Model használata közvetlenül
            result = await _billService.CreateAsync(CurrentBill);
        }

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", result.Errors[0].Description, "OK");
            return;
        }

        await Application.Current.MainPage.DisplayAlert("Siker", 
            IsEditMode ? "Számla sikeresen módosítva!" : "Számla sikeresen elmentve!", 
            "OK");

        // Reset előtt mentsük el, hogy vissza kell-e navigálni
        bool shouldNavigateBack = IsEditMode;

        // Form reset
        await ResetForm();

        // Navigáció
        if (shouldNavigateBack)
        {
            // Visszanavigálás az áttekintéshez
            await Shell.Current.GoToAsync("//MainView/BillOverview");
        }
        else
        {
            // Új számla esetén is visszamehetünk
            await Shell.Current.GoToAsync("//MainView");
        }
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
