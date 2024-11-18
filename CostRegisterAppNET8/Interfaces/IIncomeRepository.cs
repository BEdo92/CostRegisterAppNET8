using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Helpers;

namespace CostRegisterAppNET8.Interfaces;

public interface IIncomeRepository : IBaseTotalRepository<Income>
{
    Task<IEnumerable<Income>> GetIncomesAsync(string userId);
    Task<IEnumerable<CostEntryDto?>> GetIncomesAsync(string userId, CostParams costParams);
}
