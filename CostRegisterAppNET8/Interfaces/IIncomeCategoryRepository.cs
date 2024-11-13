using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface IIncomeCategoryRepository
{
    Task<int> GetCategoryIdAsync(string category);
    Task<IEnumerable<IncomeCategory>> GetIncomeCategoriesAsync();
}
