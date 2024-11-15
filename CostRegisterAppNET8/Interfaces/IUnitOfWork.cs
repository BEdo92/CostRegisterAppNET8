﻿namespace CostRegisterAppNET8.Interfaces;

public interface IUnitOfWork
{
    ICostCategoryRepository CostCategoryRepository { get; }
    ICostRepository CostRepository { get; }
    IIncomeCategoryRepository IncomeCategoryRepository { get; }
    IIncomeRepository IncomeRepository { get; }

    Task<bool> CompleteAsync();
    bool HasChanges();
}
