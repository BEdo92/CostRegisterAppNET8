using API.Data;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
public class IncomeController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult> AddIncome(CostEntryDto incomeDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var income = mapper.Map<Income>(incomeDto);
        income.AppUserId = userId;
        income.IncomeCategoryId = await unitOfWork.IncomeCategoryRepository.GetCategoryIdAsync(incomeDto.Category);

        await unitOfWork.IncomeRepository.AddAsync(income);

        if (await unitOfWork.CompleteAsync())
        {
            return Ok();
        }

        return BadRequest("Failed to add income");
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CostEntryDto>>> GetIncomes()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var incomes = await unitOfWork.IncomeRepository.GetIncomesAsync(userId);

        return Ok(mapper.Map<IEnumerable<CostEntryDto>>(incomes));
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<CostEntryDto>>> GetCosts([FromQuery] CostParams costParams)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costs = await unitOfWork.IncomeRepository.GetIncomesAsync(userId, costParams);

        return Ok(mapper.Map<IEnumerable<CostEntryDto>>(costs));
    }
}
