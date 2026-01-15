namespace Solution.WebAPI.Configurations;

public static class EnviorementVariablesConfiguration
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder LoadEnviorementVariables()
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                 .AddEnvironmentVariables();

            return builder;
        }
    }
}
