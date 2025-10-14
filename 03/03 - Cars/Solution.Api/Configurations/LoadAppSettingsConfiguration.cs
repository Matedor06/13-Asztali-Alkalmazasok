namespace REST_Sample_01.Configurations
{
    public static class LoadAppSettingsConfiguration
    {public static WebApplicationBuilder LoadAppSettingsVariables(this WebApplicationBuilder builder)
        {
            var enviroment = builder.Configuration.GetValue<string>("Environment");

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{enviroment}.json",true)
                .AddEnvironmentVariables();

            return builder;
        }
    }
}
