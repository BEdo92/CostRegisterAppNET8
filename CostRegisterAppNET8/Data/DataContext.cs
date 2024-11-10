using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<CostPlan> CostPlans { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
    public DbSet<CostCategory> CostCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
