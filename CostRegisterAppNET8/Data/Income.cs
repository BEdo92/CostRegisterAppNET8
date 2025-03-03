namespace API.Data;

public class Income : BaseTotalModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int IncomeCategoryId { get; set; }
    public IncomeCategory IncomeCategory { get; set; } = null!;
}
