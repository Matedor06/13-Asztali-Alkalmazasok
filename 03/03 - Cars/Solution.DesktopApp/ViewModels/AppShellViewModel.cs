using CommunityToolkit.Mvvm.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand AddNewCarCommand => new AsyncRelayCommand(OnAddNewCarAsync);

    public IAsyncRelayCommand AddNewTypeCommand => new AsyncRelayCommand(OnAddNewTypeAsync);

    public IAsyncRelayCommand AddNewManufacturerCommand => new AsyncRelayCommand(OnAddNewManufacturerAsync);

    public IAsyncRelayCommand ListAllManufacturersCommand => new AsyncRelayCommand(OnListAllManufacturersAsync);

    public IAsyncRelayCommand ListAllTypesCommand => new AsyncRelayCommand(OnListAllTypesAsync);

    public IAsyncRelayCommand ListAllCarsCommand => new AsyncRelayCommand(OnListAllCarsAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

    private async Task OnAddNewCarAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditCarView.Name);
    }

    private async Task OnAddNewTypeAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditTypeView.Name);
    }

    private async Task OnAddNewManufacturerAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditManufacturerView.Name);
    }

    private async Task OnListAllCarsAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CarListView.Name);
    }
    
    private async Task OnListAllManufacturersAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(ManufacturerListView.Name);
    }

    private async Task OnListAllTypesAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TypeListView.Name);
    }
}
