﻿using Application.Commands.Expense;
using Application.Commands.FinancialInvestment;
using Application.Queries.Expense;
using Application.Queries.MonetaryProjection;
using Application.Validators.Expense;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidators();

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ExpenseCommandHandler>()
            .AddScoped<FinancialInvestmentCommandHandler>();

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services
            .AddScoped<ExpenseQueryHandler>()
            .AddScoped<MonetaryProjectionQueryHandler>()
            .AddScoped<FinancialInvestmentQueryHandler>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationClientsideAdapters();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ExpenseQueryValidator>();
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        return services;
    }
}