using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CostRegisterAppNET8.Controllers;

public class CostCategoryController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostCategory>>> GetCostCategories()
    {
        return Ok(await unitOfWork.CostCategoryRepository.GetCostCategoriesAsync());
    }
}
