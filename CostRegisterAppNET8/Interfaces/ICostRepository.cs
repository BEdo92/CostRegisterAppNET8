using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface ICostRepository
{
    Task<IEnumerable<Cost>> GetCostsAsync(string userId);
    Task<Cost> GetCostByIdAsync(int id);
    Task AddCostAsync(Cost cost);
    Task UpdateCostAsync(Cost cost);
    Task DeleteCostAsync(Cost cost);
}
