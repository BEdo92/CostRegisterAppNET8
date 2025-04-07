using System.ComponentModel.DataAnnotations;

namespace API.Data;

public class Currency
{
    public int Id { get; set; }

    [Required]
    [MaxLength(3)]
    public string Code { get; set; } = string.Empty; // e.g. "HUF", "EUR", "USD"

    [MaxLength(5)]
    public string? Symbol { get; set; } // e.g. "Ft", "€", "$"

    [MaxLength(100)]
    public string? Name { get; set; } // e.g. "Hungarian Forint"

    public ICollection<UserCurrency> UserCurrencies { get; set; } = new List<UserCurrency>();
}
