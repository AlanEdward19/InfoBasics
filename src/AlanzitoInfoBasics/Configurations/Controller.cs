using System.Text.Json.Serialization;
using AlanzitoInfoBasics.Filters;

namespace AlanzitoInfoBasics.Configurations;

public static class Controller
{
    public static IServiceCollection ConfigureController(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
            //.AddFluentValidation(c =>
            //{
            //    c.RegisterValidatorsFromAssemblyContaining<TrainingValidator>();
            //    c.ValidatorOptions.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            //})

        services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        return services;
    }
}