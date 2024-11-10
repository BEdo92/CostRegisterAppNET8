using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

public class IncomeCategoryRepository(DataContext context) : IIncomeCategoryRepository
{
    public async Task<IEnumerable<IncomeCategory>> GetIncomeCategoriesAsync()
    {
        return await context.IncomeCategories.ToListAsync();
    }
}
