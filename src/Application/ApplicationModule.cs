using Application.Commands.Expense;
using Application.Queries.Expense;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries();

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ExpenseCommandHandler>();

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services
            .AddScoped<ExpenseQueryHandler>();

        return services;
    }
}