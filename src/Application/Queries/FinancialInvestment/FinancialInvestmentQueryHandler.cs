using System.Linq.Expressions;
using Application.Queries.FinancialInvestment;
using Application.ViewModels;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Expense;

public class FinancialInvestmentQueryHandler(DatabaseContext dbContext)
{
    public async Task<IEnumerable<FinancialInvestmentViewModel>> Handle(FinancialInvestmentQuery query)
    {
        var values = dbContext.FinancialInvestments.AsQueryable();

        if (!string.IsNullOrEmpty(query.Name))
            values = values.Where(x => x.Name == query.Name);

        if (!string.IsNullOrEmpty(query.Type))
            values = values.Where(x => x.Type == Enum.Parse<EFinancialInvestmentType>(query.Type));

        if (!string.IsNullOrEmpty(query.NumberOfShares))
        {
            string queryValue = query.NumberOfShares.Replace(" ", "");

            if (TryParseComparison(queryValue, out var comparison, out var number))
                values = ApplyComparison(values, x => x.NumberOfShares, comparison, number);
        }

        if (!string.IsNullOrEmpty(query.ProfitPerShare))
        {
            string queryValue = query.ProfitPerShare.Replace(" ", "");

            if (TryParseComparison(queryValue, out var comparison, out var number))
                values = ApplyComparison(values, x => x.ProfitPerShare, comparison, number);
        }

        if (!string.IsNullOrEmpty(query.TotalProfit))
        {
            string queryValue = query.TotalProfit.Replace(" ", "");

            if (TryParseComparison(queryValue, out var comparison, out var number))
                values = ApplyComparison(values, x => x.TotalProfit, comparison, number);
        }

        if (!string.IsNullOrEmpty(query.PricePerShare))
        {
            string queryValue = query.PricePerShare.Replace(" ", "");

            if (TryParseComparison(queryValue, out var comparison, out var number))
                values = ApplyComparison(values, x => x.PricePerShare, comparison, number);
        }

        if (!string.IsNullOrEmpty(query.TotalSpent))
        {
            string queryValue = query.TotalSpent.Replace(" ", "");

            if (TryParseComparison(queryValue, out var comparison, out var number))
                values = ApplyComparison(values, x => x.TotalSpent, comparison, number);
        }

        var result = FinancialInvestmentViewModel.BuildFromEntities(await values.ToListAsync());

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

    private IQueryable<Domain.Entities.FinancialInvestment> ApplyComparison<T>(
        IQueryable<Domain.Entities.FinancialInvestment> values,
        Expression<Func<Domain.Entities.FinancialInvestment, T>> propertySelector,
        EComparisonOperator comparison,
        T value)
    {
        return comparison switch
        {
            EComparisonOperator.Equals => values.Where(x => propertySelector.Compile()(x).Equals(value)),
            EComparisonOperator.LessThan => values.Where(x => Comparer<T>.Default.Compare(propertySelector.Compile()(x), value) < 0),
            EComparisonOperator.LessThanOrEqual => values.Where(x => Comparer<T>.Default.Compare(propertySelector.Compile()(x), value) <= 0),
            EComparisonOperator.GreaterThan => values.Where(x => Comparer<T>.Default.Compare(propertySelector.Compile()(x), value) > 0),
            EComparisonOperator.GreaterThanOrEqual => values.Where(x => Comparer<T>.Default.Compare(propertySelector.Compile()(x), value) >= 0),
            _ => values,
        };
    }
}