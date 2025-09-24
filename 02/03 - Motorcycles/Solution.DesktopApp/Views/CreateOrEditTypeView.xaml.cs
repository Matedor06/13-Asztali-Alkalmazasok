namespace Solution.DesktopApp.Views;

public partial class CreateOrEditTypeView : ContentPage
{
	public CreateOrEditTypeViewModel ViewModel => this.BindingContext as CreateOrEditTypeViewModel;

	public static string Name => nameof(CreateOrEditTypeView);

    public CreateOrEditTypeView(CreateOrEditTypeViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}