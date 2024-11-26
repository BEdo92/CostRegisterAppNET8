using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<CostPlan> CostPlans { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
    public DbSet<CostCategory> CostCategories { get; set; }
    public DbSet<Cost> Costs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }
}
