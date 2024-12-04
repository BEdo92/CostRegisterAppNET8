using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CostCategoryController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetCostCategories()
    {
        // TODO: Temporary solution.
        return Ok(
            (await unitOfWork.CostCategoryRepository.GetCostCategoriesAsync())
            .Select(c => c.CategoryName));
    }
}
