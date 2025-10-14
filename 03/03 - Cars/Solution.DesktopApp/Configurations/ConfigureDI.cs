namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<CarListViewModel>();
        builder.Services.AddTransient<ManufacturerListViewModel>();
        builder.Services.AddTransient<TypeListViewModel>();
        builder.Services.AddTransient<CreateOrEditCarViewModel>();
        builder.Services.AddTransient<CreateOrEditTypeViewModel>();
        builder.Services.AddTransient<CreateOrEditManufacturerViewModel>();


        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<CarListView>();
        builder.Services.AddTransient<ManufacturerListView>();
        builder.Services.AddTransient<TypeListView>();
        builder.Services.AddTransient<CreateOrEditCarView>();
        builder.Services.AddTransient<CreateOrEditTypeView>();
        builder.Services.AddTransient<CreateOrEditManufacturerView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService> ();
        builder.Services.AddTransient<ICarService, CarService>();
        builder.Services.AddTransient<ITypeService, TypeService>();
        builder.Services.AddTransient<IManufacturerService, ManufacturerService>();



        return builder;
	}
}
