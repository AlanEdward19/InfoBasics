using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


    public DbSet<Expense> Expenses { get; set; }
    public DbSet<FinancialInvestment> FinancialInvestments { get; set; }
}