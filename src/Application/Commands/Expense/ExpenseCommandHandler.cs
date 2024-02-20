using Application.ViewModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Expense;

public class ExpenseCommandHandler(DatabaseContext dbContext)
{
    public async Task<ExpenseViewModel> Insert(CreateExpenseCommand command)
    {
        var entity = command.ToEntity();

        await dbContext.Expenses.AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return ExpenseViewModel.BuildFromEntity(entity);
    }

    public async Task<ExpenseViewModel> Update(Guid id, UpdateExpenseCommand command)
    {
        var entity = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return null;

        entity.Name = command.Name;
        entity.DueDate = command.DueDate;
        entity.Frequency = command.Frequency;
        entity.Value = command.Value;
        entity.IsPaid = command.IsPaid;

        await dbContext.SaveChangesAsync();

        return ExpenseViewModel.BuildFromEntity(entity);
    }

    public async Task<ExpenseViewModel> Patch(Guid id, UpdatePartiallyExpenseCommand command)
    {
        var entity = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return null;

        
        if(!string.IsNullOrWhiteSpace(command.Name))
            entity.Name = command.Name;

        if(command.DueDate.HasValue)
            entity.DueDate = command.DueDate;

        if(command.Frequency.HasValue)
            entity.Frequency = command.Frequency.Value;

        if(command.Value.HasValue)
            entity.Value = command.Value.Value;

        if(command.IsPaid.HasValue)
            entity.IsPaid = command.IsPaid.Value;

        await dbContext.SaveChangesAsync();

        return ExpenseViewModel.BuildFromEntity(entity);
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return false;

        dbContext.Expenses.Remove(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<(bool, ExpenseViewModel)> PayOffInstallment(Guid installmentId)
    {
        var entity = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == installmentId);

        if (entity is null)
            return (false, null);

        entity.IsPaid = true;

        dbContext.Expenses.Update(entity);

        await dbContext.SaveChangesAsync();

        return (true, ExpenseViewModel.BuildFromEntity(entity));
    }
}