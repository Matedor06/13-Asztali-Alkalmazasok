using Solution.Services.Services;
using Solution.Services.Services.Bill.Validators;
using FluentValidation;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		// App
		builder.Services.AddSingleton<App>();

		// ViewModels
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<NewBillViewModel>();
		builder.Services.AddTransient<BillOverviewViewModel>();
		builder.Services.AddTransient<AppShellViewModel>();

		// Views
		builder.Services.AddTransient<MainView>();
		builder.Services.AddTransient<NewBillView>();
		builder.Services.AddTransient<BillOverviewView>();
		
		// Services
		Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddScoped<IBillService, BillService>(builder.Services);
		Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddScoped<IBillItemService, BillItemService>(builder.Services);

		// FluentValidation
		builder.Services.AddValidatorsFromAssemblyContaining<BillModelValidator>();
		builder.Services.AddHttpContextAccessor();

        return builder;
	}
}
