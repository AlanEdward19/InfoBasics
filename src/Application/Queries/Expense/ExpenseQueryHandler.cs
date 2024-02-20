using Application.ViewModels;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Expense;

public class ExpenseQueryHandler(DatabaseContext dbContext)
{
    public async Task<IEnumerable<ExpenseViewModel>> Handle(ExpenseQuery query)
    {
        var values = dbContext.Expenses.AsQueryable();

        if (!string.IsNullOrEmpty(query.Name))
        {
            values = values.Where(x => x.Name == query.Name);
        }

        if (query.DueDate.HasValue)
        {
            values = values.Where(x => x.DueDate == query.DueDate);
        }

        if (!string.IsNullOrEmpty(query.Frequency))
        {
            values = values.Where(x => x.Frequency == Enum.Parse<EFrequency>(query.Frequency));
        }

        if (!string.IsNullOrEmpty(query.Value))
        {
            string queryValue = query.Value.Replace(" ", "");

            if (TryParseComparison(queryValue, out var comparison, out var number))
                values = ApplyComparison(values, comparison, number);
        }

        var result = ExpenseViewModel.BuildFromEntities(await values.ToListAsync());

        return result;
    }

    public bool TryParseComparison(string queryValue, out EComparisonOperator comparison, out double number)
    {
        comparison = EComparisonOperator.Equals;

        if (queryValue.Contains("=="))
        {
            comparison = EComparisonOperator.Equals;
            queryValue = queryValue.Replace("==", "");
        }
        else if (queryValue.Contains("<="))
        {
            comparison = EComparisonOperator.LessThanOrEqual;
            queryValue = queryValue.Replace("<=", "");
        }
        else if (queryValue.Contains("<"))
        {
            comparison = EComparisonOperator.LessThan;
            queryValue = queryValue.Replace("<", "");
        }
        else if (queryValue.Contains(">="))
        {
            comparison = EComparisonOperator.GreaterThanOrEqual;
            queryValue = queryValue.Replace(">=", "");
        }
        else if (queryValue.Contains(">"))
        {
            comparison = EComparisonOperator.GreaterThan;
            queryValue = queryValue.Replace(">", "");
        }

        return double.TryParse(queryValue, out number);
    }

    private IQueryable<Domain.Entities.Expense> ApplyComparison(IQueryable<Domain.Entities.Expense> values, EComparisonOperator comparison, double number)
    {
        return comparison switch
        {
            EComparisonOperator.Equals => values.Where(x => x.Value == number),
            EComparisonOperator.LessThan => values.Where(x => x.Value < number),
            EComparisonOperator.LessThanOrEqual => values.Where(x => x.Value <= number),
            EComparisonOperator.GreaterThan => values.Where(x => x.Value > number),
            EComparisonOperator.GreaterThanOrEqual => values.Where(x => x.Value >= number),
            _ => values,
        };
    }
}