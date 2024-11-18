using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;
using CostRegisterAppNET8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Repositories;

public class BaseTotalRepository<TEntity>(DataContext context) : IBaseTotalRepository<TEntity>
       where TEntity : BaseTotalModel
{
    protected readonly DataContext context = context;
    private DbSet<TEntity> dbSet = context.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<decimal> GetTotalAsync()
    {
        return await dbSet.SumAsync(x => x.Total);
    }

    /// <summary>
    /// Filters the query that is already filtered by userId and category.
    /// </summary>
    protected async Task<IEnumerable<CostEntryDto?>> FilterAsync(IQueryable<CostEntryDto> query, CostParams costParams)
    {
        if (!string.IsNullOrEmpty(costParams.Category) && costParams.Category != "all")
        {
            query = query.Where(x => x.Category == costParams.Category);
        }

        if (costParams.TotalFrom is not null)
        {
            query = query.Where(x => x.Total >= costParams.TotalFrom);
        }

        if (costParams.TotalTo is not null)
        {
            query = query.Where(x => x.Total <= costParams.TotalTo);
        }

        if (costParams.DateFrom is not null)
        {
            query = query.Where(x => x.Date >= costParams.DateFrom);
        }

        if (costParams.DateTo is not null)
        {
            query = query.Where(x => x.Date <= costParams.DateTo);
        }

        query = costParams.OrderBy switch
        {
            "date" => query.OrderByDescending(x => x.Date),
            _ => query.OrderByDescending(x => x.Total)
        };

        return await PagedList<CostEntryDto>.CreateAsync(query, costParams.PageNumber, costParams.PageSize);
    }

    public void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }
}
