using Application.Commands.FinancialInvestment;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.FinancialInvestment;

public class UpdatePartiallyFinancialInvestmentCommandValidator : AbstractValidator<UpdatePartiallyFinancialInvestmentCommand>
{
    public UpdatePartiallyFinancialInvestmentCommandValidator()
    {
        RuleFor(x => x.Frequency)
            .Must(x => Enum.TryParse<EFrequency>(x, out _))
            .WithMessage(x =>
                $"InvalidValue: '{x.Frequency}' isn't a valid value for Frequency. Use one of the following values: {GetEnumValuesString<EFrequency>()}")
            .When(x => !string.IsNullOrWhiteSpace(x.Frequency));

        RuleFor(x => x.Type)
            .Must(x => Enum.TryParse<EFinancialInvestmentType>(x, out _))
            .WithMessage(x =>
                $"InvalidValue: '{x.Type}' isn't a valid value for Type. Use one of the following values: {GetEnumValuesString<EFinancialInvestmentType>()}")
            .When(x => !string.IsNullOrWhiteSpace(x.Type));

        RuleFor(x => x.PurchaseDay)
            .GreaterThan(0)
            .WithMessage("PurchaseDay must be greater than 0")
            .When(x => x.PurchaseDay.HasValue);

        RuleFor(x => x.PurchaseDay)
            .LessThan(31)
            .WithMessage("PurchaseDay must be less than 31")
            .When(x => x.PurchaseDay.HasValue);

        RuleFor(x => x.PayDay)
            .GreaterThan(0)
            .WithMessage("PayDay must be greater than 0")
            .When(x => x.PayDay.HasValue);

        RuleFor(x => x.PayDay)
            .LessThan(31)
            .WithMessage("PayDay must be less than 31")
            .When(x => x.PayDay.HasValue);
    }

    static string GetEnumValuesString<TEnum>() where TEnum : Enum
    {
        string[] enumStrings = Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(value => $"{value.ToString()}")
            .ToArray();

        return string.Join(", ", enumStrings);
    }
}