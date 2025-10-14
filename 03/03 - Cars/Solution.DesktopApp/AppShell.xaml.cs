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
        Routing.RegisterRoute(MainView.Name, typeof(MainView));
        Routing.RegisterRoute(CarListView.Name, typeof(CarListView));
        Routing.RegisterRoute(ManufacturerListView.Name, typeof(ManufacturerListView));
        Routing.RegisterRoute(TypeListView.Name, typeof(TypeListView));
        Routing.RegisterRoute(CreateOrEditCarView.Name, typeof(CreateOrEditCarView));
        Routing.RegisterRoute(CreateOrEditTypeView.Name, typeof(CreateOrEditTypeView));
        Routing.RegisterRoute(CreateOrEditManufacturerView.Name, typeof(CreateOrEditManufacturerView));
    }
}
