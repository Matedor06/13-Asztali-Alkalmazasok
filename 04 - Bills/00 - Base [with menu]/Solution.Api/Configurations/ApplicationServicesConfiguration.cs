namespace Solution.Api.Configurations;

public static class ApplicationServicesConfiguration
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // Add Database Configuration
        builder = builder.ConfigureDatabase();
        
        // Add Services
        builder.Services.AddScoped<IBillService, BillService>();
        builder.Services.AddScoped<IBillItemService, BillItemService>();
        
        return builder;
    }
}
