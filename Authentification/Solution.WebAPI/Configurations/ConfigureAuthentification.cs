using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Solution.WebAPI.Configurations;

public static class ConfigureAuthentication
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder UseSecurity()
        {
            var settings = builder.Configuration.GetSection("JWT").Get<JWTSettingsModel>();

            builder.Services.AddAuthorization(options =>
            {
                var defaultAuthoraizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthoraizationPolicyBuilder = defaultAuthoraizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthoraizationPolicyBuilder.Build();
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                };
            });
            return builder;
        }
    }
    extension(WebApplication app)
    {
        public IApplicationBuilder UseSecurity()
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
