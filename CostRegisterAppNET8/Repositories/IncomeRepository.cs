﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

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
