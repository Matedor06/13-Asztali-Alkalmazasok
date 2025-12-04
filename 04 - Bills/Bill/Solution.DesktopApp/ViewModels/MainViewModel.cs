namespace Solution.DesktopApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public const string Name = nameof(MainViewModel);

    [RelayCommand]
    private async Task NavigateToNewBill()
    {
        await Shell.Current.GoToAsync("NewBill");
    }

    [RelayCommand]
    private async Task NavigateToBillOverview()
    {
        await Shell.Current.GoToAsync("BillOverview");
    }
}
