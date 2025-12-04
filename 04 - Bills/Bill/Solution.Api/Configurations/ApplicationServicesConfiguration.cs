namespace Solution.Api.Configurations;

public static class ApplicationServicesConfiguration
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        // Add Controllers with FluentValidation
        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var result = new
                    {
                        title = "One or more validation errors occurred.",
                        status = 400,
                        errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // Add FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<BillModelValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateBillDtoValidator>();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddHttpContextAccessor();
        
        // Add Database Configuration
        builder = builder.ConfigureDatabase();
        
        // Add Services
        builder.Services.AddScoped<IBillService, BillService>();
        builder.Services.AddScoped<IBillItemService, BillItemService>();
        
        return builder;
    }
}
