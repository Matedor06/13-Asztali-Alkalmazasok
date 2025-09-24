namespace Solution.DesktopApp.Views;

public partial class CreateOrEditManufacturerView : ContentPage
{
	public CreateOrEditManufacturerViewModel ViewModel => this.BindingContext as CreateOrEditManufacturerViewModel;

	public static string Name => nameof(CreateOrEditManufacturerView);

    public CreateOrEditManufacturerView(CreateOrEditManufacturerViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}