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
            .ToListAsync();
    }

    public async Task<IEnumerable<CostEntryDto?>> GetIncomesAsync(string userId, CostParams costParams)
    {
        var query = context.Incomes
            .Where(c => c.AppUserId == userId)
            .Include(c => c.IncomeCategory)
            .ProjectTo<CostEntryDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await FilterAsync(query, costParams);
    }
}
