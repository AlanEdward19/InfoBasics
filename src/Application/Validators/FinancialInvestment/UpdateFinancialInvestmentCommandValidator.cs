using Application.Commands.FinancialInvestment;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.FinancialInvestment;

public class UpdateFinancialInvestmentCommandValidator : AbstractValidator<UpdateFinancialInvestmentCommand>
{
    public UpdateFinancialInvestmentCommandValidator()
    {

        RuleFor(x => x.Frequency)
            .NotEmpty()
            .WithMessage("Frequency can't be empty");

        RuleFor(x => x.Frequency)
            .Must(x => Enum.TryParse<EFrequency>(x, out _))
            .WithMessage(x =>
                $"InvalidValue: '{x.Frequency}' isn't a valid value for Frequency. Use one of the following values: {GetEnumValuesString<EFrequency>()}");

        RuleFor(x => x.NumberOfShares)
            .NotNull()
            .WithMessage("NumberOfShares is required");

        RuleFor(x => x.NumberOfShares)
            .NotEmpty()
            .WithMessage("NumberOfShares can't be empty");

        RuleFor(x => x.ProfitPerShare)
            .NotNull()
            .WithMessage("ProfitPerShare is required");

        RuleFor(x => x.ProfitPerShare)
            .NotEmpty()
            .WithMessage("ProfitPerShare can't be empty");

        RuleFor(x => x.PricePerShare)
            .NotNull()
            .WithMessage("PricePerShare is required");

        RuleFor(x => x.PricePerShare)
            .NotEmpty()
            .WithMessage("PricePerShare can't be empty");

        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage("Type is required");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type can't be empty");

        RuleFor(x => x.Type)
            .Must(x => Enum.TryParse<EFinancialInvestmentType>(x, out _))
            .WithMessage(x =>
                $"InvalidValue: '{x.Type}' isn't a valid value for Type. Use one of the following values: {GetEnumValuesString<EFinancialInvestmentType>()}");

        RuleFor(x => x.PurchaseDay)
            .NotNull()
            .WithMessage("PurchaseDay is required");

        RuleFor(x => x.PurchaseDay)
            .NotEmpty()
            .WithMessage("PurchaseDay can't be empty");

        RuleFor(x => x.PurchaseDay)
            .GreaterThan(0)
            .WithMessage("PurchaseDay must be greater than 0");

        RuleFor(x => x.PurchaseDay)
            .LessThan(31)
            .WithMessage("PurchaseDay must be less than 31");

        RuleFor(x => x.PayDay)
            .NotNull()
            .WithMessage("PayDay is required");

        RuleFor(x => x.PayDay)
            .NotEmpty()
            .WithMessage("PayDay can't be empty");

        RuleFor(x => x.PayDay)
            .GreaterThan(0)
            .WithMessage("PayDay must be greater than 0");

        RuleFor(x => x.PayDay)
            .LessThan(31)
            .WithMessage("PayDay must be less than 31");

        RuleFor(x => x.Url)
            .NotNull()
            .WithMessage("Url is required");

        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("Url can't be empty");
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