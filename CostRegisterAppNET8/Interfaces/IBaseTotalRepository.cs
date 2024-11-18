﻿using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface IBaseTotalRepository<TEntity> where TEntity : BaseTotalModel
{
    Task<TEntity?> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task<decimal> GetTotalAsync();
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
