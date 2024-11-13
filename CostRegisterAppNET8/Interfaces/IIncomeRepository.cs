using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface IIncomeRepository
{
    Task AddIncomeAsync(Income income);
    Task<IEnumerable<Income>> GetIncomesAsync(string userId);
}
