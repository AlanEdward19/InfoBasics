using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class FinancialInvestment
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfShares { get; set; }
    public double ProfitPerShare { get; set; }
    public double TotalProfit { get; set; }
    public double PricePerShare { get; set; }
    public EFinancialInvestmentType Type { get; set; }
    public int PurchaseDay { get; set; }
    public int PayDay { get; set; }
    public EFrequency Frequency { get; set; }
    public double TotalSpent { get; set; }
    public string Url { get; set; }
}