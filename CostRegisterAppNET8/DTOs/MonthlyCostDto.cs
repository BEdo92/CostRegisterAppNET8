namespace API.DTOs;

public class MonthlyCostDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalCost { get; set; }
}
