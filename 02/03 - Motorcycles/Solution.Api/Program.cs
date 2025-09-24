using REST_Sample_01.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.LoadAppSettingsVariables()
       .ConfigureDatabase()
       .ConfigureDI()
       .ConfigureFluentValidation();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
