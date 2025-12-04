namespace Solution.DesktopApp.Configurations;

public static class ConfigureAppVariables
{
	public static MauiAppBuilder UseAppConfigurations(this MauiAppBuilder builder)
	{
#if DEBUG
        var fileName = "appSettings.Development.json";
#else
        var fileName = "appSettings.Production.json";
#endif

        // Try to load from embedded resources
        try
        {
            using var stream = FileSystem.Current.OpenAppPackageFileAsync(fileName).Result;
            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load configuration file {fileName}: {ex.Message}");
        }

		return builder;
	}
}
