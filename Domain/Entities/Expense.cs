using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Expense
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? DueDate { get; set; }
    public EFrequency Frequency { get; set; }
    public double Value { get; set; }
    public bool IsPaid { get; set; }
}