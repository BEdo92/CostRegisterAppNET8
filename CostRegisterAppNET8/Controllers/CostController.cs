using AutoMapper;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;
using CostRegisterAppNET8.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CostRegisterAppNET8.Controllers;

[Authorize]
public class CostController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<CostDto>> AddCost(CostDto costDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var cost = mapper.Map<Cost>(costDto);
        cost.AppUserId = userId;

        await unitOfWork.CostRepository.AddCostAsync(cost);

        if (await unitOfWork.CompleteAsync())
        {
            return Ok();
        }

        return BadRequest("Failed to add cost");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostDto>>> GetCosts()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costs = await unitOfWork.CostRepository.GetCostsAsync(userId);

        return Ok(mapper.Map<IEnumerable<CostDto>>(costs));
    }
}
