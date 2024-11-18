using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;

namespace CostRegisterAppNET8.Repositories;

public class UnitOfWork(
    DataContext context,
    ICostCategoryRepository costCategoryRepository,
    IIncomeCategoryRepository incomeCategoryRepository,
    ICostRepository costRepository,
    IIncomeRepository incomeRepository,
    ICostplanRepository costplanRepository) : IUnitOfWork
{
    public ICostCategoryRepository CostCategoryRepository => costCategoryRepository;

    public ICostRepository CostRepository => costRepository;

    public IIncomeCategoryRepository IncomeCategoryRepository => incomeCategoryRepository;
    public IIncomeRepository IncomeRepository => incomeRepository;
    public ICostplanRepository CostplanRepository => costplanRepository;

    public async Task<bool> CompleteAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
