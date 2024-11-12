using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CostRegisterAppNET8.Controllers;

public class IncomeCategoryController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostCategory>>> GetIncomeCategories()
    {
        return Ok(await unitOfWork.IncomeCategoryRepository.GetIncomeCategoriesAsync());
    }
}
