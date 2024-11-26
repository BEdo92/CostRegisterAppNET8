using AutoMapper;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

public class BalanceController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<decimal>> GetBalance()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var incomes = await unitOfWork.IncomeRepository.GetIncomesAsync(userId);
        var costs = await unitOfWork.CostRepository.GetCostsAsync(userId);

        var totalIncome = incomes.Sum(i => i.Total);
        var totalCost = costs.Sum(c => c.Total);

        var balance = totalIncome - totalCost;

        return Ok(balance);
    }
}
