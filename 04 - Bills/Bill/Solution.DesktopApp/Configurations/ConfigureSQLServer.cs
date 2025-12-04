namespace Solution.DesktopApp.Configurations;

public static class ConfigureSQLServer
{
	public static MauiAppBuilder UseMsSqlServer(this MauiAppBuilder builder)
	{	
		// Direkt connection string használata, ha a konfiguráció nem elérhető
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
			?? "Data Source=(LocalDB)\\MSSQLLocalDB;Database=BillDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

		builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly("Solution.Database");
				sqlOptions.EnableRetryOnFailure();
			}));

		return builder;
	}
}
