namespace API.Interfaces;

public interface ICurrencyRepository
{
    Task<List<string>> GetAllCurrenciesAsync();
    Task<int> GetCurrencyId(string? currencyName);
}
