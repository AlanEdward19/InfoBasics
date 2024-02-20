using Application.Commands.Expense;
using Application.ViewModels;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.FinancialInvestment;

public class FinancialInvestmentCommandHandler(DatabaseContext dbContext)
{
    public async Task<FinancialInvestmentViewModel> Insert(CreateFinancialInvestmentCommand command)
    {
        int numberOfShares = command.NumberOfShares.Value;
        var entity = command.ToEntity(CalculateTotalSpent(numberOfShares, command.PricePerShare.Value),
            CalculateTotalProfit(numberOfShares, command.ProfitPerShare.Value));

        await dbContext.FinancialInvestments.AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return FinancialInvestmentViewModel.BuildFromEntity(entity);
    }

    public async Task<FinancialInvestmentViewModel> BuySellShares(Guid id, BuySellSharesCommand command)
    {
        var entity = await dbContext.FinancialInvestments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return null;

        int numberOfShares = command.Amount;

        if (command.Sell)
            entity.NumberOfShares -= numberOfShares;

        else
            entity.NumberOfShares += numberOfShares;

        entity.TotalSpent = CalculateTotalSpent(numberOfShares, entity.PricePerShare);
        entity.TotalProfit = CalculateTotalProfit(numberOfShares, entity.ProfitPerShare);

        await dbContext.SaveChangesAsync();

        return FinancialInvestmentViewModel.BuildFromEntity(entity);
    }

    public async Task<FinancialInvestmentViewModel> Update(Guid id, UpdateFinancialInvestmentCommand command)
    {
        var entity = await dbContext.FinancialInvestments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return null;

        int numberOfShares = command.NumberOfShares.Value;

        entity.Name = command.Name!;
        entity.NumberOfShares = command.NumberOfShares.Value;
        entity.Frequency = Enum.Parse<EFrequency>(command.Frequency!);
        entity.PayDay = command.PayDay.Value;
        entity.PricePerShare = command.PricePerShare.Value;
        entity.ProfitPerShare = command.ProfitPerShare.Value;
        entity.PurchaseDay = command.PurchaseDay.Value;
        entity.Type = Enum.Parse<EFinancialInvestmentType>(command.Type!);
        entity.Url = command.Url!;
        entity.TotalProfit = CalculateTotalProfit(numberOfShares, command.ProfitPerShare.Value);
        entity.TotalSpent = CalculateTotalSpent(numberOfShares, command.PricePerShare.Value);

        await dbContext.SaveChangesAsync();

        return FinancialInvestmentViewModel.BuildFromEntity(entity);
    }

    public async Task<FinancialInvestmentViewModel> Patch(Guid id, UpdatePartiallyFinancialInvestmentCommand command)
    {
        var entity = await dbContext.FinancialInvestments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return null;

        if (!string.IsNullOrWhiteSpace(command.Name))
            entity.Name = command.Name;

        if (!string.IsNullOrWhiteSpace(command.Frequency))
            entity.Frequency = Enum.Parse<EFrequency>(command.Frequency);

        if (command.PayDay.HasValue)
            entity.PayDay = command.PayDay.Value;

        if (command.PricePerShare.HasValue)
            entity.PricePerShare = command.PricePerShare.Value;

        if (command.ProfitPerShare.HasValue)
            entity.ProfitPerShare = command.ProfitPerShare.Value;

        if (command.PurchaseDay.HasValue)
            entity.PurchaseDay = command.PurchaseDay.Value;

        if (!string.IsNullOrWhiteSpace(command.Type))
            entity.Type = Enum.Parse<EFinancialInvestmentType>(command.Type);

        if (!string.IsNullOrWhiteSpace(command.Url))
            entity.Url = command.Url;

        entity.TotalProfit = CalculateTotalProfit(entity.NumberOfShares, entity.ProfitPerShare);
        entity.TotalSpent = CalculateTotalSpent(entity.NumberOfShares, entity.PricePerShare);

        await dbContext.SaveChangesAsync();

        return FinancialInvestmentViewModel.BuildFromEntity(entity);
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await dbContext.FinancialInvestments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return false;

        dbContext.FinancialInvestments.Remove(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }

    private double CalculateTotalProfit(int numberOfShares, double profitPerShare) => numberOfShares * profitPerShare;
    private double CalculateTotalSpent(int numberOfShares, double pricePerShare) => numberOfShares * pricePerShare;
}