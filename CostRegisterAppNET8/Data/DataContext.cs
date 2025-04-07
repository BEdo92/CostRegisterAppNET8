using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<CostPlan> CostPlans { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
    public DbSet<CostCategory> CostCategories { get; set; }
    public DbSet<Cost> Costs { get; set; }
    public DbSet<UserCurrency> UserCurrencies { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<UserCurrency>()
            .HasOne(uc => uc.User)
            .WithOne()
            .HasForeignKey<UserCurrency>(uc => uc.UserId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
