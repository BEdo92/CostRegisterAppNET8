using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class IncomeCategoryRepository(DataContext context) : IIncomeCategoryRepository
{
    public async Task<int> GetCategoryIdAsync(string category)
    {
        return await context.IncomeCategories
            .Where(c => c.CategoryName == category)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<IncomeCategory>> GetIncomeCategoriesAsync()
    {
        return await context.IncomeCategories.ToListAsync();
    }
}
