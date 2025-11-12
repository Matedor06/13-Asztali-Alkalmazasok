namespace Solution.Api.Configurations;

public static class LoadAppSettingsConfiguration
{
    public static WebApplicationBuilder LoadAppSettingsVariables(this WebApplicationBuilder builder)
    {
        var environment = builder.Configuration.GetValue<string>("Environment");

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        return builder;
    }
}
