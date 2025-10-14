namespace Solution.DesktopApp.ViewModels;

public partial class CreateOrEditCarViewModel(
    AppDbContext dbContext,
    ICarService carService,
    IGoogleDriveService googleDriveService) : CarModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    public IRelayCommand ValidateCommand => new AsyncRelayCommand<string>(OnValidateAsync);

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
    #endregion

    private CarModelValidator validator => new CarModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private IList<ManufacturerModel> manufacturers = [];

    [ObservableProperty]
    private IList<TypeModel> types = [];

    [ObservableProperty]
    private IList<int> doors = [2, 3, 4, 5];

    [ObservableProperty]
    private ImageSource image;

    private FileResult selectedFile = null;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(LoadManufacturersAsync);
        await Task.Run(LoadTypesAsync);

        bool hasValue = query.TryGetValue("Car", out object result);

        if(!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new car";
            return;
        }

        CarModel car = result as CarModel;

        this.Id = car.Id;
        
        // Find the matching Manufacturer from the loaded list
        this.Manufacturer = Manufacturers.FirstOrDefault(m => m.Id == car.Manufacturer.Id) ?? car.Manufacturer;
        
        // Find the matching Type from the loaded list
        this.Type = Types.FirstOrDefault(t => t.Id == car.Type.Id) ?? car.Type;
        
        this.Model = car.Model;
        this.ReleaseYear = car.ReleaseYear;
        this.Cubic = car.Cubic;
        this.NumberOfDoors = car.NumberOfDoors;
        this.ImageId = car.ImageId;
        this.WebContentLink = car.WebContentLink;

        if(!string.IsNullOrEmpty(car.WebContentLink))
        {
            Image = new UriImageSource
            {
                Uri = new Uri(car.WebContentLink),
                CacheValidity = new TimeSpan(10, 0, 0, 0)
            };
        }

        asyncButtonAction = OnUpdateAsync;
        Title = "Update car";
    }

    private async Task OnAppearingAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async Task OnSaveAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        await UploadImageAsync();

        var result = await carService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Car saved.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        await UploadImageAsync();

        var result = await carService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Car updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select the car image"
        });

        if(selectedFile is null)
        {
            return;
        }

        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private async Task UploadImageAsync()
    {
        if (selectedFile is null)
        {
            return;
        }

        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);

        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "Car image uploaded.";
        var title = imageUploadResult.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;
    }

    private async Task LoadManufacturersAsync()
    {
        Manufacturers = await dbContext.Manufacturers.AsNoTracking()
                                                     .OrderBy(x => x.Name)
                                                     .Select(x => new ManufacturerModel(x))
                                                     .ToListAsync();
    }

    private async Task LoadTypesAsync()
    {
        Types = await dbContext.Types.AsNoTracking()
                                     .OrderBy(x => x.Name)
                                     .Select(x => new TypeModel(x))
                                     .ToListAsync();
    }

    private void ClearForm()
    {
        this.Manufacturer = new ManufacturerModel();
        this.Type = new TypeModel();
        this.Model = null;
        this.Cubic = null;
        this.ReleaseYear = null;
        this.NumberOfDoors = null;

        this.Image = null;
        this.selectedFile = null;
        this.WebContentLink = null;
        this.ImageId = null;
    }

    private async Task OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));
        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == CarModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }
}