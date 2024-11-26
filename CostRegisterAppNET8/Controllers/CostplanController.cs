using AutoMapper;
using API.Data;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
public class CostplanController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult> AddCostplan(CostEntryDto incomeDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costplan = mapper.Map<CostPlan>(incomeDto);
        costplan.AppUserId = userId;
        costplan.CostCategoryId = await unitOfWork.CostCategoryRepository.GetCategoryIdAsync(incomeDto.Category);

        await unitOfWork.CostplanRepository.AddAsync(costplan);

        if (await unitOfWork.CompleteAsync())
        {
            return Ok();
        }

        return BadRequest("Failed to add income");
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CostEntryDto>>> GetCostplans()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costplans = await unitOfWork.CostplanRepository.GetCostplansAsync(userId);

        return Ok(mapper.Map<IEnumerable<CostEntryDto>>(costplans));
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<CostEntryDto>>> GetCosts([FromQuery] CostParams costParams)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costs = await unitOfWork.CostplanRepository.GetCostplansAsync(userId, costParams);

        return Ok(mapper.Map<IEnumerable<CostEntryDto>>(costs));
    }
}
