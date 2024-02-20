using Domain.Enums;

namespace Application.Commands.Expense;

public class UpdatePartiallyExpenseCommand
{
    public string? Name { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Frequency { get; set; }
    public double? Value { get; set; }
    public bool? IsPaid { get; set; }
}