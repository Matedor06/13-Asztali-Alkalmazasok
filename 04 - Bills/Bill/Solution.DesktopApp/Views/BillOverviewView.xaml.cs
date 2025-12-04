namespace Solution.DesktopApp.Views;

public partial class BillOverviewView : ContentPage
{
    public BillOverviewViewModel ViewModel => BindingContext as BillOverviewViewModel;

    public BillOverviewView(BillOverviewViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel?.OnAppearing();
    }
}
