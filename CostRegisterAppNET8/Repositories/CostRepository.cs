using API.Data;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CostRepository : BaseTotalRepository<Cost>, ICostRepository
{
    private readonly IMapper mapper;

    public CostRepository(DataContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Cost>> GetCostsAsync(string userId)
    {
        return await context.Costs
            .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<CostEntryDto?>> GetCostsAsync(string userId, CostParams costParams)
    {
        var query = context.Costs
            .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .AsNoTracking()
            .ProjectTo<CostEntryDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await FilterAsync(query, costParams);
    }

    public async Task<decimal> GetTotalByUserIdAsync(string userId)
    {
        return await context.Costs
            .Where(c => c.AppUserId == userId)
            .AsNoTracking()
            .SumAsync(c => c.Total);
    }

    public async Task<List<CategoryShareDto>> GetCategorySharesAsync(string userId)
    {
        var totalIncome = await context.Costs
            .Where(c => c.AppUserId == userId)
            .SumAsync(i => i.Total);

        if (totalIncome == 0)
        {
            return [];
        }

        var categoryShares = await context.Costs
            .Where(c => c.AppUserId == userId)
            .GroupBy(i => i.CostCategory)
            .Select(g => new CategoryShareDto
            {
                CategoryName = g.Key.CategoryName,
                Percentage = (g.Sum(i => i.Total) / totalIncome) * 100
            })
            .ToListAsync();

        return categoryShares;
    }

    public async Task<List<MonthlyCostDto>> GetMonthlyCostsAsync(string userId)
    {
        var monthlyCosts = await context.Costs
            .Where(c => c.AppUserId == userId)
            .GroupBy(c => new { c.Date.Year, c.Date.Month })
            .Select(g => new MonthlyCostDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalCost = g.Sum(c => c.Total)
            })
            .OrderBy(m => m.Year)
            .ThenBy(m => m.Month)
            .ToListAsync();

        return monthlyCosts;
    }
}
