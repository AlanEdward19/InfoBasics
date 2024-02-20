namespace Application.ViewModels;

public class MonetaryProjectionViewModel
{
    public string Date { get; set; }
    public double TotalInAccount { get; set; }
    public double TotalExpenses { get; set; }
    public double InvestmentsProfit { get; set; }
    public double TotalInAccountAfterInvestments { get; set; }

    public MonetaryProjectionViewModel() { }

    public MonetaryProjectionViewModel(string date, double totalInAccount, double totalExpenses, double investmentsProfit, double totalInAccountAfterInvestments)
    {
        Date = date;
        TotalInAccount = totalInAccount;
        TotalExpenses = totalExpenses;
        InvestmentsProfit = investmentsProfit;
        TotalInAccountAfterInvestments = totalInAccountAfterInvestments;
    }
}