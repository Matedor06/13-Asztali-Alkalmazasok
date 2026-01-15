using Solution.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.LoadEnvironmentVariables()
        .ConfigureDI()
        .ConfigureDatabase()
        .UseSecurity()
        .UseIdentity()
        .LoadSettings();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSecurity();

app.MapControllers();

app.Run();
