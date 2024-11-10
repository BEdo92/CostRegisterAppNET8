using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface IIncomeCategoryRepository
{
    Task<IEnumerable<IncomeCategory>> GetIncomeCategoriesAsync();
}
