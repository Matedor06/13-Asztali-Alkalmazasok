namespace Solution.DesktopApp.Views;

public partial class NewBillView : ContentPage
{
    public NewBillViewModel ViewModel => BindingContext as NewBillViewModel;

    public NewBillView(NewBillViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Check for navigation parameters
        if (Shell.Current.CurrentState?.Location?.OriginalString?.Contains("Bill") == true)
        {
            // Navigation parameter handling would go here
            // For now, we'll keep it simple
        }
    }
}
