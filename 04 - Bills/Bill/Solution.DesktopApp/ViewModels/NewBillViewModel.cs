using FluentValidation;

namespace Solution.DesktopApp.ViewModels;

public partial class NewBillViewModel : ObservableObject
{
    public const string Name = nameof(NewBillViewModel);

    private readonly IBillService _billService;
    private readonly IBillItemService _billItemService;
    private readonly IValidator<BillModel> _billValidator;
    private readonly IValidator<BillItemModel> _billItemValidator;

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
    private string pageTitle = "Új Számla Létrehozása";

    public NewBillViewModel(IBillService billService, IBillItemService billItemService, IValidator<BillModel> billValidator, IValidator<BillItemModel> billItemValidator)
    {
        _billService = billService;
        _billItemService = billItemService;
        _billValidator = billValidator;
        _billItemValidator = billItemValidator;
    }

    partial void OnIsEditModeChanged(bool value)
    {
        PageTitle = value ? "Számla Szerkesztése" : "Új Számla Létrehozása";
    }

    [RelayCommand]
    private async Task AddOrUpdateBillItem()
    {
        
        var validationResult = await _billItemValidator.ValidateAsync(CurrentBillItem);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            await Application.Current.MainPage.DisplayAlert("Hiba", errorMessages, "OK");
            return;
        }

        if (IsItemEditMode && EditingItemIndex >= 0)
        {
            
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
            
            BillItems.Add(new BillItemModel
            {
                Name = CurrentBillItem.Name,
                UnitPrice = CurrentBillItem.UnitPrice,
                Quantity = CurrentBillItem.Quantity
            });
        }

        
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
        
        CurrentBill.BillItems = new ObservableCollection<BillItemModel>(BillItems);

        
        var validationResult = await _billValidator.ValidateAsync(CurrentBill);
        
        if (!validationResult.IsValid)
        {
            
            var errorMessages = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            await Application.Current.MainPage.DisplayAlert("Validációs hiba", errorMessages, "OK");
            return;
        }

        ErrorOr<BillModel> result;

        if (IsEditMode && CurrentBill.Id > 0)
        {
           
            result = await _billService.UpdateAsync(CurrentBill);
        }
        else
        {
          
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

     
        bool shouldNavigateBack = IsEditMode;

      
        await ResetForm();

       
        if (shouldNavigateBack)
        {
           
            await Shell.Current.GoToAsync("//MainView/BillOverview");
        }
        else
        {
         
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
