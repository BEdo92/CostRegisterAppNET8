﻿using API.Data;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces;

public interface ICostRepository : IBaseTotalRepository<Cost>
{
    Task<IEnumerable<Cost>> GetCostsAsync(string userId);
    Task<IEnumerable<CostEntryDto?>> GetCostsAsync(string userId, CostParams costParams);
}
