using API.Data;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class IncomeRepository : BaseTotalRepository<Income>, IIncomeRepository
{
    private readonly IMapper mapper;

    public IncomeRepository(DataContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Income>> GetIncomesAsync(string userId)
    {
        return await context.Incomes
            .Where(c => c.AppUserId == userId)
            .Include(c => c.IncomeCategory)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<CostEntryDto?>> GetIncomesAsync(string userId, CostParams costParams)
    {
        var query = context.Incomes
            .Where(c => c.AppUserId == userId)
            .Include(c => c.IncomeCategory)
            .AsNoTracking()
            .ProjectTo<CostEntryDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await FilterAsync(query, costParams);
    }

    public async Task<decimal> GetTotalByUserIdAsync(string userId)
    {
        return await context.Incomes
            .Where(c => c.AppUserId == userId)
            .AsNoTracking()
            .SumAsync(c => c.Total);
    }

    public async Task<List<CategoryShareDto>> GetCategorySharesAsync(string userId)
    {
        var totalIncome = await context.Incomes
            .Where(c => c.AppUserId == userId)
            .SumAsync(i => i.Total);

        if (totalIncome == 0)
        {
            return new List<CategoryShareDto>();
        }

        var categoryShares = await context.Incomes
            .Where(c => c.AppUserId == userId)
            .GroupBy(i => i.IncomeCategory)
            .Select(g => new CategoryShareDto
            {
                CategoryName = g.Key.CategoryName,
                Percentage = (g.Sum(i => i.Total) / totalIncome) * 100
            })
            .ToListAsync();

        return categoryShares;
    }
}
