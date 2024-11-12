namespace CostRegisterAppNET8.DTOs;

public class CostDto
{
    public required string CostCategory { get; set; }
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
}
