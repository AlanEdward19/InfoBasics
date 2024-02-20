using Domain.Entities;

namespace Application.ViewModels;

public class FinancialInvestmentViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfShares { get; set; }
    public double ProfitPerShare { get; set; }
    public double TotalProfit { get; set; }
    public double PricePerShare { get; set; }
    public string Type { get; set; }
    public int PurchaseDay { get; set; }
    public int PayDay { get; set; }
    public string Frequency { get; set; }
    public double TotalSpent { get; set; }
    public string Url { get; set; }

    public static FinancialInvestmentViewModel BuildFromEntity(FinancialInvestment financialInvestment)
    {
        FinancialInvestmentViewModel result = new()
        {
            Id = financialInvestment.Id,
            Name = financialInvestment.Name,
            Frequency = financialInvestment.Frequency.ToString(),
            NumberOfShares = financialInvestment.NumberOfShares,
            PayDay = financialInvestment.PayDay,
            PricePerShare = financialInvestment.PricePerShare,
            ProfitPerShare = financialInvestment.ProfitPerShare,
            PurchaseDay = financialInvestment.PurchaseDay,
            TotalProfit = financialInvestment.TotalProfit,
            TotalSpent = financialInvestment.TotalSpent,
            Type = financialInvestment.Type.ToString(),
            Url = financialInvestment.Url
        };

        return result;
    }

    public static IEnumerable<FinancialInvestmentViewModel> BuildFromEntities(IEnumerable<FinancialInvestment> financialInvestments)
    {
        return financialInvestments.Select(BuildFromEntity);
    }
}