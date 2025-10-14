namespace Solution.DesktopApp.Views;

public partial class CreateOrEditCarView : ContentPage
{
	public CreateOrEditCarViewModel ViewModel => this.BindingContext as CreateOrEditCarViewModel;

	public static string Name => nameof(CreateOrEditCarView);

    public CreateOrEditCarView(CreateOrEditCarViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}