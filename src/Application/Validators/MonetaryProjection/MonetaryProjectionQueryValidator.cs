using Application.Queries.MonetaryProjection;
using FluentValidation;

namespace Application.Validators.MonetaryProjection;

public class MonetaryProjectionQueryValidator : AbstractValidator<MonetaryProjectionQuery>
{
    public MonetaryProjectionQueryValidator()
    {
        RuleFor(x => x.NumberOfMonths)
            .GreaterThan(0)
            .WithMessage("NumberOfMonths must have a value greater than 0");
    }
}