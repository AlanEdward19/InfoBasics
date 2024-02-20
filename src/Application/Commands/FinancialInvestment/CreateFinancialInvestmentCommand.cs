using Domain.Enums;

namespace Application.Commands.FinancialInvestment;

public class CreateFinancialInvestmentCommand
{
    public string? Name { get; set; }
    public int? NumberOfShares { get; set; }
    public double? ProfitPerShare { get; set; }
    public double? PricePerShare { get; set; }
    public string? Type { get; set; }
    public int? PurchaseDay { get; set; }
    public int? PayDay { get; set; }
    public string? Frequency { get; set; }
    public string? Url { get; set; }

    public Domain.Entities.FinancialInvestment ToEntity(double totalSpent, double totalProfit)
    {
        return new ()
        {
            Id = Guid.NewGuid(),
            Name = Name.ToUpper(),
            NumberOfShares = NumberOfShares.Value,
            ProfitPerShare = ProfitPerShare.Value,
            TotalProfit = totalProfit,
            PricePerShare = PricePerShare.Value,
            Type = Enum.Parse<EFinancialInvestmentType>(Type),
            PurchaseDay = PurchaseDay.Value,
            PayDay = PayDay.Value,
            Frequency = Enum.Parse<EFrequency>(Frequency),
            TotalSpent = totalSpent,
            Url = Url,
        };
    }
}