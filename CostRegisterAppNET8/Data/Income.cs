namespace CostRegisterAppNET8.Data;

public class Income : BaseTotalModel
{
    public int Id { get; set; }
    public int IncomeCategoryId { get; set; }
    public IncomeCategory IncomeCategory { get; set; } = null!;
}
