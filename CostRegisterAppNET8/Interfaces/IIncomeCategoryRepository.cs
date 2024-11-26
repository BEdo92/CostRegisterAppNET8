using API.Data;

namespace API.Interfaces;

public interface IIncomeCategoryRepository
{
    Task<int> GetCategoryIdAsync(string category);
    Task<IEnumerable<IncomeCategory>> GetIncomeCategoriesAsync();
}
