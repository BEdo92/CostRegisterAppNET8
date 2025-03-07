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
}
