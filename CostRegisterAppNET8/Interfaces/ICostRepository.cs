using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;

namespace CostRegisterAppNET8.Interfaces;

public interface ICostRepository : IBaseTotalRepository<Cost>
{
    Task<IEnumerable<Cost>> GetCostsAsync(string userId);
    Task<IEnumerable<CostEntryDto?>> GetCostsAsync(string userId, CostParams costParams);
}
