using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

public class CostRepository(DataContext context) : ICostRepository
{
    public async Task AddCostAsync(Cost cost)
    {
        await context.Costs.AddAsync(cost);
    }

    public Task DeleteCostAsync(Cost cost)
    {
        throw new NotImplementedException();
    }

    public Task<Cost> GetCostByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Cost>> GetCostsAsync(string userId)
    {
        return await context.Costs
            .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .ToListAsync();
    }

    public Task UpdateCostAsync(Cost cost)
    {
        throw new NotImplementedException();
    }
}
