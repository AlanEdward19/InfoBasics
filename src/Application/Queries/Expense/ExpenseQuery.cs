namespace Application.Queries.Expense;

public class ExpenseQuery
{
    public string? Name { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Frequency { get; set; }
    public string? Value { get; set; }
    public bool? IsPaid { get; set; }
}