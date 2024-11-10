using System.ComponentModel.DataAnnotations;

namespace CostRegisterAppNET8.DTOs;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(15, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string? Email { get; set; }
}
