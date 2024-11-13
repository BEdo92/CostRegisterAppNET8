using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

public class IncomeRepository(DataContext context) : IIncomeRepository
{
    public async Task AddIncomeAsync(Income income)
    {
        await context.Incomes.AddAsync(income);
    }

    public async Task<IEnumerable<Income>> GetIncomesAsync(string userId)
    {
        return await context.Incomes.Where(i => i.AppUserId == userId).ToListAsync();
    }
}
