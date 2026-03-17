using HeroWars.Database;
using Microsoft.EntityFrameworkCore;

var connectionString =
    "Data Source=(LocalDB)\\MSSQLLocalDB;Database=HeroWarsDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
