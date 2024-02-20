using Application.ViewModels;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.MonetaryProjection;

public class MonetaryProjectionQueryHandler(DatabaseContext dbContext)
{
    public async Task<List<MonetaryProjectionViewModel>> Handle(MonetaryProjectionQuery query)
    {
        List<MonetaryProjectionViewModel> result = new();

        var expenses = await dbContext.Expenses.Where(x => x.IsPaid == false).ToListAsync();

        DateTime date = DateTime.Today;

        double totalInAccount = query.InitialAmount;

        int i = 0;

        while (i < query.NumberOfMonths)
        {
            date = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1); // Define date como o último dia do mês corrente

            if (i == 0 && !query.AlreadyPaid || i != 0)
                totalInAccount += query.MonthlyIncome;

            #region Monthly Expenses

            double totalExpenses = expenses
                .Where(x => (x.DueDate >= date || !x.DueDate.HasValue) && x.Frequency == EFrequency.Monthly)
                .Sum(x => x.Value);

            #endregion

            #region Weekly Expenses

            var weeklyExpenses = expenses
                .Where(x => (x.DueDate >= date || !x.DueDate.HasValue) && x.Frequency == EFrequency.Weekly)
                .Sum(x => x.Value * 4);

            totalExpenses += weeklyExpenses;

            #endregion

            #region Yearly Expenses

            var yearlyExpenses = expenses
                .Where(x => (x.DueDate >= date || !x.DueDate.HasValue) && x.Frequency == EFrequency.Yearly)
                .Sum(x => x.Value / 12);

            totalExpenses += yearlyExpenses;

            #endregion

            totalInAccount -= totalExpenses;

            MonetaryProjectionViewModel item = new(date.ToString("MMMM, yyyy"), double.Round(totalInAccount, 2),
                double.Round(totalExpenses, 2), 0, 0);
            result.Add(item);

            date = date.AddMonths(1);

            i++;
        }

        return result;
    }
}