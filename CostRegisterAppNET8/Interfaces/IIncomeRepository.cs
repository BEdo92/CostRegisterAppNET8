using API.Data;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces;

public interface IIncomeRepository : IBaseTotalRepository<Income>
{
    Task<List<CategoryShareDto>> GetCategorySharesAsync(string userId);
    Task<IEnumerable<Income>> GetIncomesAsync(string userId);
    Task<IEnumerable<CostEntryDto?>> GetIncomesAsync(string userId, CostParams costParams);
    Task<decimal> GetTotalByUserIdAsync(string userId);
}
