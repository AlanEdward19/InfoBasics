using Domain.Enums;

namespace Application.Queries.MonetaryProjection;

public class MonetaryProjectionQuery
{
    public double InitialAmount { get; set; }
    public double MonthlyIncome { get; set; }
    public bool AlreadyPaid { get; set; }
    public int NumberOfMonths { get; set; }
}