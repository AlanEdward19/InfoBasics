using Application;
using Infrastructure;

namespace AlanzitoInfoBasics.Configurations;

public static class IoC
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region CORS

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        #endregion

        services
            .AddInfrastructure(configuration)
            .AddApplication();

        return services;
    }
}