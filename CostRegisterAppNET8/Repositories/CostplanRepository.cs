using API.Data;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CostplanRepository : BaseTotalRepository<CostPlan>, ICostplanRepository
{
    private readonly IMapper mapper;

    public CostplanRepository(DataContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CostPlan>> GetCostplansAsync(string userId)
    {
        return await context.CostPlans
            .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<CostEntryDto?>> GetCostplansAsync(string userId, CostParams costParams)
    {
        var query = context.CostPlans
        .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .AsNoTracking()
            .ProjectTo<CostEntryDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await FilterAsync(query, costParams);
    }
}