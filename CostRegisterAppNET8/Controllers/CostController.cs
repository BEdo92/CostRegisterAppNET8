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
public class CostController(IUnitOfWork unitOfWork, IMapper mapper) : BaseTotalController<Cost>(unitOfWork)
{
    [HttpPost]
    public async Task<ActionResult<CostEntryDto>> AddCost(CostEntryDto costDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var cost = mapper.Map<Cost>(costDto);
        cost.AppUserId = userId;
        cost.CostCategoryId = await unitOfWork.CostCategoryRepository.GetCategoryIdAsync(costDto.Category);

        await unitOfWork.CostRepository.AddAsync(cost);

        if (await unitOfWork.CompleteAsync())
        {
            return Ok();
        }

        return BadRequest("Failed to add cost");
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CostEntryDto>>> GetCosts()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costs = await unitOfWork.CostRepository.GetCostsAsync(userId);

        return Ok(mapper.Map<IEnumerable<CostEntryDto>>(costs));
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<CostEntryDto>>> GetCosts([FromQuery] CostParams costParams)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        var costs = await unitOfWork.CostRepository.GetCostsAsync(userId, costParams);

        return Ok(mapper.Map<IEnumerable<CostEntryDto>>(costs));
    }

    protected override async Task<Cost?> GetEntityByIdAsync(int id)
    {
        return await unitOfWork.CostRepository.GetByIdAsync(id);
    }

    protected override bool IsUserAuthorized(Cost entity, string userId)
    {
        return entity.AppUserId == userId;
    }

    protected override void DeleteEntity(Cost entity)
    {
        unitOfWork.CostRepository.Delete(entity);
    }
}
