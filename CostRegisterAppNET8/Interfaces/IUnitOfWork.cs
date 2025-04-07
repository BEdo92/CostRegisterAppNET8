namespace API.Interfaces;

public interface IUnitOfWork
{
    ICostCategoryRepository CostCategoryRepository { get; }
    ICostRepository CostRepository { get; }
    IIncomeCategoryRepository IncomeCategoryRepository { get; }
    IIncomeRepository IncomeRepository { get; }
    ICostplanRepository CostplanRepository { get; }
    IUserCurrencyRepository UserCurrencyRepository { get; }
    ICurrencyRepository CurrencyRepository { get; }

    Task<bool> CompleteAsync();
    bool HasChanges();
}
