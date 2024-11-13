using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface ICostCategoryRepository
{
    Task<int> GetCategoryIdAsync(string category);
    Task<IEnumerable<CostCategory>> GetCostCategoriesAsync();
}
