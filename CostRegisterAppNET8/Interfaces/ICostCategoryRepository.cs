using API.Data;

namespace API.Interfaces;

public interface ICostCategoryRepository
{
    Task<int> GetCategoryIdAsync(string category);
    Task<IEnumerable<CostCategory>> GetCostCategoriesAsync();
}
