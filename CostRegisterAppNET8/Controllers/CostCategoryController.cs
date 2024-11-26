using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CostCategoryController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostCategory>>> GetCostCategories()
    {
        return Ok(await unitOfWork.CostCategoryRepository.GetCostCategoriesAsync());
    }
}
