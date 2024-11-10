using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface ICostCategoryRepository
{
    Task<IEnumerable<CostCategory>> GetCostCategoriesAsync();
}
