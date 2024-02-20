using Application.Queries.Expense;
using Application.Queries.FinancialInvestment;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.FinancialInvestment;

public class FinancialInvestmentQueryValidator : AbstractValidator<FinancialInvestmentQuery>
{
    public FinancialInvestmentQueryValidator(FinancialInvestmentQueryHandler queryHandler)
    {
        RuleFor(x => x.Frequency)
            .Must(x => Enum.TryParse<EFrequency>(x, out _))
            .WithMessage("Frequency must be a valid value")
            .When(x => !string.IsNullOrWhiteSpace(x.Frequency));

        RuleFor(x => x.NumberOfShares)
            .Must(x => queryHandler.TryParseComparison(x, out _, out _))
            .WithMessage("NumberOfShares must be a number, or a combination of number and comparison operator")
            .When(x => !string.IsNullOrWhiteSpace(x.NumberOfShares));

        RuleFor(x => x.PricePerShare)
            .Must(x => queryHandler.TryParseComparison(x, out _, out _))
            .WithMessage("PricePerShare must be a number, or a combination of number and comparison operator")
            .When(x => !string.IsNullOrWhiteSpace(x.PricePerShare));

        RuleFor(x => x.ProfitPerShare)
            .Must(x => queryHandler.TryParseComparison(x, out _, out _))
            .WithMessage("ProfitPerShare must be a number, or a combination of number and comparison operator")
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitPerShare));

        RuleFor(x => x.TotalProfit)
            .Must(x => queryHandler.TryParseComparison(x, out _, out _))
            .WithMessage("TotalProfit must be a number, or a combination of number and comparison operator")
            .When(x => !string.IsNullOrWhiteSpace(x.TotalProfit));

        RuleFor(x => x.TotalSpent)
            .Must(x => queryHandler.TryParseComparison(x, out _, out _))
            .WithMessage("TotalSpent must be a number, or a combination of number and comparison operator")
            .When(x => !string.IsNullOrWhiteSpace(x.TotalSpent));

        RuleFor(x => x.Type)
            .Must(x => Enum.TryParse<EFinancialInvestmentType>(x, out _))
            .WithMessage("Type must be a valid value")
            .When(x => !string.IsNullOrWhiteSpace(x.Type));
    }
}