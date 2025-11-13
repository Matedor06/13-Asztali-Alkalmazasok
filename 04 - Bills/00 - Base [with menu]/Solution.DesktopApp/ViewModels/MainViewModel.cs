namespace Solution.DesktopApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public const string Name = nameof(MainViewModel);

    [RelayCommand]
    private async Task NavigateToNewBill()
    {
        await Shell.Current.GoToAsync(NewBillViewModel.Name);
    }

    [RelayCommand]
    private async Task NavigateToBillOverview()
    {
        await Shell.Current.GoToAsync(BillOverviewViewModel.Name);
    }
}
