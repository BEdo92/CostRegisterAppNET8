using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class IncomeCategoryController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetIncomeCategories()
    {
        return Ok((await unitOfWork.IncomeCategoryRepository.GetIncomeCategoriesAsync())
            .Select(i => i.CategoryName));
    }
}
