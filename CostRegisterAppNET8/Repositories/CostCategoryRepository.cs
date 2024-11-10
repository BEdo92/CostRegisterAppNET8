using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

public class CostCategoryRepository(DataContext context) : ICostCategoryRepository
{
    public async Task<IEnumerable<CostCategory>> GetCostCategoriesAsync()
    {
        return await context.CostCategories.ToListAsync();
    }
}
