namespace CostRegisterAppNET8.DTOs;

public class CostEntryDto
{
    public required string Category { get; set; }
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
}
