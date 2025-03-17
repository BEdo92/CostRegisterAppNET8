using API.Interfaces;
using AutoMapper;
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

        var income = await unitOfWork.IncomeRepository.GetTotalByUserIdAsync(userId);
        var costs = await unitOfWork.CostRepository.GetTotalByUserIdAsync(userId);

        var balance = income - costs;

        return Ok(balance);
    }

    [HttpGet("withplan")]
    public async Task<ActionResult<decimal>> GetBalanceWithCostPlans()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var income = await unitOfWork.IncomeRepository.GetTotalByUserIdAsync(userId);
        var costs = await unitOfWork.CostRepository.GetTotalByUserIdAsync(userId);
        var costplans = await unitOfWork.CostplanRepository.GetTotalByUserIdAsync(userId);

        var balance = income - costs - costplans;

        return Ok(balance);
    }
}
