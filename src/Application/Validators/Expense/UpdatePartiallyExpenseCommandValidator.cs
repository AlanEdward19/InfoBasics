using Application.Commands.Expense;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators.Expense;

public class UpdatePartiallyExpenseCommandValidator : AbstractValidator<UpdatePartiallyExpenseCommand>
{
	public UpdatePartiallyExpenseCommandValidator()
    {
        RuleFor(x => x.Frequency)
            .Must(x => Enum.TryParse<EFrequency>(x, out _))
            .WithMessage(x =>
                $"InvalidValue: '{x.Frequency}' isn't a valid value for Frequency. Use one of the following values: {GetEnumValuesString()}")
            .When(x => !string.IsNullOrWhiteSpace(x.Frequency));

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0")
            .When(x => x.Value.HasValue);
    }

    private static string GetEnumValuesString()
    {
        string[] enumStrings = Enum.GetValues(typeof(EFrequency))
            .Cast<EFrequency>()
            .Select(value => $"{value.ToString()}")
            .ToArray();

        return string.Join(", ", enumStrings);
    }
}