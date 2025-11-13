namespace Solution.DesktopApp;

public partial class AppShell : Shell
{
    public AppShellViewModel ViewModel => this.BindingContext as AppShellViewModel;

    public AppShell(AppShellViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();

        ConfigureShellNavigation();
    }

    private static void ConfigureShellNavigation()
    {
        Routing.RegisterRoute(nameof(MainViewModel), typeof(MainView));
        Routing.RegisterRoute(nameof(NewBillViewModel), typeof(NewBillView));
        Routing.RegisterRoute(nameof(BillOverviewViewModel), typeof(BillOverviewView));
    }
}
