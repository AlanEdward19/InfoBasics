using Application.Queries.Expense;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Expense;

public class ExpenseQueryValidator : AbstractValidator<ExpenseQuery>
{
    public ExpenseQueryValidator(ExpenseQueryHandler queryHandler)
    {
        RuleFor(x => x.Frequency)
            .Must(x => Enum.TryParse<EFrequency>(x, out _))
            .WithMessage("Frequency must be a valid value")
            .When(x => !string.IsNullOrWhiteSpace(x.Frequency));

        RuleFor(x => x.Value)
            .Must(x => queryHandler.TryParseComparison(x, out _, out _))
            .WithMessage("Value must be a number, or a combination of number and comparison operator")
            .When(x => !string.IsNullOrWhiteSpace(x.Value));
    }
}