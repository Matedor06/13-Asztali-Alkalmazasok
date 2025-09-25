using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DesktopApp.ViewModels;

public partial class CreateOrEditManufacturerViewModel(
    AppDbContext dbContext,
    IManufacturerService manufacturerService) : ManufacturerModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    public IRelayCommand ValidateCommand => new AsyncRelayCommand<string>(OnValidateAsync);

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    #endregion

    private ManufacturerModelValidator validator => new ManufacturerModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Manufacturer", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            this.title = "Add new manufacturer";
            return;
        }

        ManufacturerModel manufacturer = result as ManufacturerModel;

        this.Id = manufacturer.Id;
        this.Name = manufacturer.Name;

        asyncButtonAction = OnUpdateAsync;
        this.title = "Update manufacturer";
    }

    private async Task OnAppearingAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { 
    }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async Task OnSaveAsync()
    {
        this.validationResult = await validator.ValidateAsync(this);

        if (!this.validationResult.IsValid)
        {
            return;
        }

        var result = await manufacturerService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Manufacturer saved.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        this.validationResult = await validator.ValidateAsync(this);

        if (!this.validationResult.IsValid)
        {
            return;
        }

        var result = await manufacturerService.UpdateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Manufacturer updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name = null;
    }

    private async Task OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        this.validationResult.Errors.Remove(this.validationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));
        this.validationResult.Errors.Remove(this.validationResult.Errors.FirstOrDefault(x => x.PropertyName == ManufacturerModelValidator.GlobalProperty));
        this.validationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }
}