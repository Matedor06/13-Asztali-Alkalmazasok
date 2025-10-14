namespace Solution.DesktopApp.Views;

public partial class CarListView : ContentPage
{
	public CarListViewModel ViewModel => this.BindingContext as CarListViewModel;

	public static string Name => nameof(CarListView);
	
	public CarListView(CarListViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
    }
}