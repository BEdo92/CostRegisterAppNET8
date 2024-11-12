namespace CostRegisterAppNET8.Interfaces;

public interface IUnitOfWork
{
    ICostCategoryRepository CostCategoryRepository { get; }
    ICostRepository CostRepository { get; }
    IIncomeCategoryRepository IncomeCategoryRepository { get; }

    Task<bool> CompleteAsync();
    bool HasChanges();
}
