using API.Data;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces;

public interface ICostplanRepository : IBaseTotalRepository<CostPlan>
{
    Task<IEnumerable<CostPlan>> GetCostplansAsync(string userId);
    Task<IEnumerable<CostEntryDto?>> GetCostplansAsync(string userId, CostParams costParams);
    Task<decimal> GetTotalByUserIdAsync(string userId);
}
