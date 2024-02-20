using Domain.Entities;
using Domain.Enums;

namespace Application.ViewModels;

public class ExpenseViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? DueDate { get; set; }
    public int? RemainingInstallments { get; set; }
    public string Frequency { get; set; }
    public double Value { get; set; }
    public bool IsPaid { get; set; }


    public static ExpenseViewModel BuildFromEntity(Expense expense)
    {
        ExpenseViewModel result = new()
        {
            Id = expense.Id,
            Name = expense.Name,
            DueDate = expense.DueDate,
            RemainingInstallments = expense.DueDate.HasValue ? CalculateRemainingInstallments(expense.Frequency, (DateTime)expense.DueDate) : null,
            Frequency = expense.Frequency.ToString(),
            Value = expense.Value,
            IsPaid = expense.IsPaid
        };

        return result;
    }

    public static IEnumerable<ExpenseViewModel> BuildFromEntities(IEnumerable<Expense> expenses)
    {
        return expenses.Select(BuildFromEntity);
    }

    private static int? CalculateRemainingInstallments(EFrequency frequency, DateTime dueDate)
    {
        DateTime currentDate = DateTime.Now.Date;

        int monthsDifference = ((dueDate.Year - currentDate.Year) * 12) + dueDate.Month - currentDate.Month;

        int remainingInstallments = 0;

        switch (frequency)
        {
            case EFrequency.Weekly:
                remainingInstallments = (int)Math.Ceiling((double)monthsDifference * 4.34524);
                break;

            case EFrequency.Monthly:
                remainingInstallments = monthsDifference;
                break;

            case EFrequency.Yearly:
                remainingInstallments = monthsDifference / 12;
                break;


            default:
                throw new ArgumentOutOfRangeException(nameof(frequency));
        }

        return remainingInstallments;
    }

}