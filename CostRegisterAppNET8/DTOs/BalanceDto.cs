﻿namespace API.DTOs;

public class BalanceDto
{
    public decimal Balance { get; set; }
    public decimal BalanceIncludedCostPlans { get; set; }

    public string? CurrencyCode { get; set; }
    public string? CurrencySymbol { get; set; }

    public List<CategoryShareDto> IncomeCategoryShares { get; set; }
    public List<CategoryShareDto> CostCategoryShares { get; set; }

    public List<MonthlyCostDto> MonthlyCosts { get; set; }
}
