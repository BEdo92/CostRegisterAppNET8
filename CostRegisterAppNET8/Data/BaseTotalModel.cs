namespace API.Data;

public abstract class BaseTotalModel
{
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public string? Comment { get; set; }
    public AppUser? AppUser { get; set; }
    public string? AppUserId { get; set; }
}
