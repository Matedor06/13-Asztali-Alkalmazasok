namespace Solution.DesktopApp.ViewModels;

public partial class BillOverviewViewModel : ObservableObject
{
    public const string Name = nameof(BillOverviewViewModel);

    private readonly IBillService _billService;
    private const int PageSize = 20;

    [ObservableProperty]
    private ObservableCollection<BillModel> bills = new();

    [ObservableProperty]
    private ObservableCollection<BillModel> displayedBills = new();

    [ObservableProperty]
    private int currentPage = 1;

    [ObservableProperty]
    private int totalPages = 1;

    [ObservableProperty]
    private bool hasPreviousPage;

    [ObservableProperty]
    private bool hasNextPage;

    [ObservableProperty]
    private string pageInfo = "1 / 1 oldal";

    public BillOverviewViewModel(IBillService billService)
    {
        _billService = billService;
    }

    [RelayCommand]
    private async Task LoadBills()
    {
        var result = await _billService.GetAllAsync();

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", result.Errors[0].Description, "OK");
            return;
        }

        Bills = new ObservableCollection<BillModel>(result.Value.OrderByDescending(b => b.DateIssued));
        CalculatePagination();
        LoadPage();
    }

    private void CalculatePagination()
    {
        TotalPages = (int)Math.Ceiling((double)Bills.Count / PageSize);
        if (TotalPages == 0) TotalPages = 1;
    }

    private void LoadPage()
    {
        var itemsToDisplay = Bills
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        DisplayedBills = new ObservableCollection<BillModel>(itemsToDisplay);

        HasPreviousPage = CurrentPage > 1;
        HasNextPage = CurrentPage < TotalPages;
        PageInfo = $"{CurrentPage} / {TotalPages} oldal";
    }

    [RelayCommand]
    private void NextPage()
    {
        if (HasNextPage)
        {
            CurrentPage++;
            LoadPage();
        }
    }

    [RelayCommand]
    private void PreviousPage()
    {
        if (HasPreviousPage)
        {
            CurrentPage--;
            LoadPage();
        }
    }

    [RelayCommand]
    private void FirstPage()
    {
        CurrentPage = 1;
        LoadPage();
    }

    [RelayCommand]
    private void LastPage()
    {
        CurrentPage = TotalPages;
        LoadPage();
    }

    [RelayCommand]
    private async Task DeleteBill(BillModel bill)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Figyelmeztetés",
            $"Biztosan törölni szeretné a(z) '{bill.BillNumber}' számlát?",
            "Igen",
            "Nem");

        if (!confirm) return;

        var result = await _billService.DeleteAsync(bill.Id);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", result.Errors[0].Description, "OK");
            return;
        }

        await Application.Current.MainPage.DisplayAlert("Siker", "Számla sikeresen törölve!", "OK");
        await LoadBills();
    }

    [RelayCommand]
    private async Task EditBill(BillModel bill)
    {
        // Betöltjük a teljes számlát a tételekkel együtt
        var result = await _billService.GetByIdAsync(bill.Id);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", result.Errors[0].Description, "OK");
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            { "Bill", result.Value }
        };

        await Shell.Current.GoToAsync($"{NewBillViewModel.Name}", parameters);
    }

    [RelayCommand]
    private async Task NavigateToNewBill()
    {
        await Shell.Current.GoToAsync(NewBillViewModel.Name);
    }

    public async Task OnAppearing()
    {
        await LoadBills();
    }
}
