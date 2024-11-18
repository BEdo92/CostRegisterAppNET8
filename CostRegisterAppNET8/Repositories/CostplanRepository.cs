using AutoMapper;
using AutoMapper.QueryableExtensions;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

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
            .ToListAsync();
    }

    public async Task<IEnumerable<CostEntryDto?>> GetCostplansAsync(string userId, CostParams costParams)
    {
        var query = context.CostPlans
        .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .ProjectTo<CostEntryDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await FilterAsync(query, costParams);
    }
}