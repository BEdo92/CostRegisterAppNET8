using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

public class BalanceController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<BalanceDto>> GetBalance()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        decimal income = await unitOfWork.IncomeRepository.GetTotalByUserIdAsync(userId);
        decimal costs = await unitOfWork.CostRepository.GetTotalByUserIdAsync(userId);
        decimal costPlans = await unitOfWork.CostplanRepository.GetTotalByUserIdAsync(userId);

        decimal balance = income - costs;
        decimal balanceIncludedCostPlans = income - costs - costPlans;

        var incomeCategoryShares = await unitOfWork.IncomeRepository.GetCategorySharesAsync(userId);
        var expenseCategoryShares = await unitOfWork.CostRepository.GetCategorySharesAsync(userId);

        // 3. Get Monthly Costs
        var monthlyCosts = await unitOfWork.CostRepository.GetMonthlyCostsAsync(userId);

        var balanceDto = new BalanceDto
        {
            Balance = balance,
            BalanceIncludedCostPlans = balanceIncludedCostPlans,
            IncomeCategoryShares = incomeCategoryShares,
            CostCategoryShares = expenseCategoryShares,
            MonthlyCosts = monthlyCosts
        };

        return Ok(balanceDto);
    }
}
