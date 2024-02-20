namespace Application.Commands.FinancialInvestment;

public class UpdatePartiallyFinancialInvestmentCommand
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
}