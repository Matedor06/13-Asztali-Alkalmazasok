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
        // Regisztráljuk az oldalakat route néven
        Routing.RegisterRoute("MainView", typeof(MainView));
        Routing.RegisterRoute("NewBill", typeof(NewBillView));
        Routing.RegisterRoute("BillOverview", typeof(BillOverviewView));
    }
}
