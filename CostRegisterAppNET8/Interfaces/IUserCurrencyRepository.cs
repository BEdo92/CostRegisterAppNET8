namespace API.Interfaces;

public interface IUserCurrencyRepository
{
    Task AddUserCurrencyAsync(string userId, int currencyCode);
    Task<string> GetUserCurrencyCodeAsync(string userId);
    Task<string> GetUserCurrencySymbolAsync(string userId);
}
