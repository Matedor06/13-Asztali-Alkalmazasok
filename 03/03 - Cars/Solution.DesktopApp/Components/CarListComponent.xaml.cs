namespace Solution.DesktopApp.Components;

public partial class CarListComponent : ContentView
{
    public static readonly BindableProperty CarProperty = BindableProperty.Create(
         propertyName: nameof(Car),
         returnType: typeof(CarModel),
         declaringType: typeof(CarListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public CarModel Car
    {
        get => (CarModel)GetValue(CarProperty);
        set => SetValue(CarProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(CarListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: nameof(CommandParameter),
         returnType: typeof(string),
         declaringType: typeof(CarListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public CarListComponent()
	{
		InitializeComponent();
	}

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Car", this.Car}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditCarView.Name, navigationQueryParameter);
    }
}