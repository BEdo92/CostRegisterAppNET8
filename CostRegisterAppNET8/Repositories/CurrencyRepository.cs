using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CurrencyRepository(DataContext context) : ICurrencyRepository
{
    public async Task<List<string>> GetAllCurrenciesAsync()
    {
        return await context.Currencies
                             .Select(c => c.Code)
                             .ToListAsync();
    }

    public async Task<int> GetCurrencyId(string? currencyCode)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            return 0;
        }

        var currency = await context.Currencies
                                     .FirstOrDefaultAsync(c => c.Code == currencyCode);
        return currency?.Id ?? 0;
    }
}
