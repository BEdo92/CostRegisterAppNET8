using AutoMapper;
using AutoMapper.QueryableExtensions;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

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
            .ToListAsync();
    }

    public async Task<IEnumerable<CostEntryDto?>> GetCostsAsync(string userId, CostParams costParams)
    {
        var query = context.Costs
            .Where(c => c.AppUserId == userId)
            .Include(c => c.CostCategory)
            .ProjectTo<CostEntryDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await FilterAsync(query, costParams);
    }
}
