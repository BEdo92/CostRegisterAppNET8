namespace CostRegisterAppNET8.Helpers;

public class CostParams
{
    private const int MaxPageSize = 20;
    public int PageNumber { get; set; } = 1;
    private int pageSize = 10;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public string? Category { get; set; }
    public decimal? TotalFrom { get; set; }
    public decimal? TotalTo { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public string OrderBy { get; set; } = "date";
}
