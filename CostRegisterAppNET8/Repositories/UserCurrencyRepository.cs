using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserCurrencyRepository(DataContext context) : IUserCurrencyRepository
{
    public async Task AddUserCurrencyAsync(string userId, int currencyId)
    {
        var userCurrency = new UserCurrency
        {
            UserId = userId,
            CurrencyId = currencyId
        };
        await context.UserCurrencies.AddAsync(userCurrency);
    }

    public async Task<string> GetUserCurrencyCodeAsync(string userId)
    {
        var userCurrency = await context.UserCurrencies
            .Include(uc => uc.Currency)
            .FirstOrDefaultAsync(uc => uc.UserId == userId);

        return userCurrency?.Currency?.Code ?? "EUR"; // NOTE: Default to "EUR" if not found.
    }

    public async Task<string> GetUserCurrencySymbolAsync(string userId)
    {
        var userCurrency = await context.UserCurrencies
            .Include(uc => uc.Currency)
            .FirstOrDefaultAsync(uc => uc.UserId == userId);

        return userCurrency?.Currency?.Symbol ?? "€"; // NOTE: Default to "EUR" if not found.
    }
}
