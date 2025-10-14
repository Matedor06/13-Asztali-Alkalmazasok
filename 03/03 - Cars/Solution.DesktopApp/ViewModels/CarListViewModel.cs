namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CarListViewModel(ICarService carService)
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region paging commands
    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }
    #endregion

    #region component commands
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<string>((id) => OnDeleteAsync(id));
    #endregion

    [ObservableProperty]
    private ObservableCollection<CarModel> cars;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfCarsInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadCarsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadCarsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadCarsAsync();
    }

    private async Task LoadCarsAsync()
    {
        isLoading = true;

        var result = await carService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Cars not loaded!", "OK");
            return;
        }

        Cars = new ObservableCollection<CarModel>(result.Value.Items);
        numberOfCarsInDB = result.Value.Count;

        hasNextPage = numberOfCarsInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    { 
        var result = await carService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Car deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var car = cars.SingleOrDefault(x => x.Id == id);
            cars.Remove(car);

            if(cars.Count == 0)
            {
                await LoadCarsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}