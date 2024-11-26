namespace API.DTOs;

public class CostEntryDto
{
    public required string Category { get; set; }
    public required decimal Total { get; set; }
    public required DateTime Date { get; set; }
}
