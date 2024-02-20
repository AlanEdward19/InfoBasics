using AlanzitoInfoBasics.Middlewares;

namespace AlanzitoInfoBasics.Configurations;

public static class Middleware
{
    public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}