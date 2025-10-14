using Solution.Core.Interfaces;
using Solution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class ManufacturerListViewModel(IManufacturerService manufacturerService)
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
    private ObservableCollection<ManufacturerModel> manufacturers;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfMotorcyclesInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);
        await LoadManufacturersAsync();

    }
    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadManufacturersAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadManufacturersAsync();
    }

    private async Task LoadManufacturersAsync()
    {
        isLoading = true;

        var result = await manufacturerService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Motorcycles not loaded!", "OK");
            return;
        }

        Manufacturers = new ObservableCollection<ManufacturerModel>(result.Value.Items);
        numberOfMotorcyclesInDB = result.Value.Count;

        hasNextPage = numberOfMotorcyclesInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    {
        var result = await manufacturerService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Motorcycle deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var manufacturer = manufacturers.SingleOrDefault(x => x.Id == id);
            manufacturers.Remove(manufacturer);

            if (manufacturers.Count == 0)
            {
                await LoadManufacturersAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
