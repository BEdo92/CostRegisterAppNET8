using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CurrencyController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetCurrencies()
    {
        var currencies = await unitOfWork.CurrencyRepository.GetAllCurrenciesAsync();
        return Ok(currencies);
    }
}
