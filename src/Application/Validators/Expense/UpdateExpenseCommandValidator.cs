using Application.Commands.Expense;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators.Expense;

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
	public UpdateExpenseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Name is required");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty");

        RuleFor(x => x.Frequency)
            .NotNull()
            .WithMessage("Frequency is required");

        RuleFor(x => x.Frequency)
            .NotEmpty()
            .WithMessage("Frequency can't be empty");

        RuleFor(x => x.Frequency)
            .Must(x => Enum.TryParse<EFrequency>(x, out _))
            .WithMessage(x =>
                $"InvalidValue: '{x.Frequency}' isn't a valid value for Frequency. Use one of the following values: {GetEnumValuesString()}");

        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage("Value is required");

        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Value can't be empty");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0");

        RuleFor(x => x.IsPaid)
            .NotNull()
            .WithMessage("IsPaid is required");

        RuleFor(x => x.IsPaid)
            .NotEmpty()
            .WithMessage("IsPaid can't be empty");
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