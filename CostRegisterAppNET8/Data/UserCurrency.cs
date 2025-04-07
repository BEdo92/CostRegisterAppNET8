using System.ComponentModel.DataAnnotations;

namespace API.Data;

public class UserCurrency
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public int CurrencyId { get; set; }

    public Currency Currency { get; set; } = null!;

    public AppUser User { get; set; } = null!;
}
