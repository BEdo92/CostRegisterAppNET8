using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;

namespace CostRegisterAppNET8.Interfaces;

public interface ICostplanRepository : IBaseTotalRepository<CostPlan>
{
    Task<IEnumerable<CostPlan>> GetCostplansAsync(string userId);
    Task<IEnumerable<CostEntryDto?>> GetCostplansAsync(string userId, CostParams costParams);
}
