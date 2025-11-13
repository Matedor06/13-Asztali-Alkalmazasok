namespace Solution.DesktopApp.ViewModels;

public partial class AppShellViewModel : ObservableObject
{
    [RelayCommand]
    private async Task ExitApplication()
    {
        Application.Current.Quit();
    }

    [RelayCommand]
    private async Task NavigateToNewBill()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainViewModel)}/{nameof(NewBillViewModel)}");
    }

    [RelayCommand]
    private async Task NavigateToBillOverview()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainViewModel)}/{nameof(BillOverviewViewModel)}");
    }
}
