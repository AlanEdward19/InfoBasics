using Domain.Entities;
using Domain.Enums;

namespace Application.Commands.Expense;

public class CreateExpenseCommand
{
    public string? Name { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Frequency { get; set; }
    public double? Value { get; set; }

    public Domain.Entities.Expense ToEntity()
    {
        return new Domain.Entities.Expense
        {
            Id = Guid.NewGuid(),
            Name = Name,
            DueDate = DueDate,
            Frequency = Enum.Parse<EFrequency>(Frequency),
            Value = Value.Value,
            IsPaid = false
        };
    }
}