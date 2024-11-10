namespace CostRegisterAppNET8.Data;

public class Income : BaseAmountModel
{
    public int Id { get; set; }
    public int IncomeCategoryId { get; set; }
    public IncomeCategory? IncomeCategory { get; set; }
}
