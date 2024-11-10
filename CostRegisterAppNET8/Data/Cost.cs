namespace CostRegisterAppNET8.Data;

public class Cost : BaseAmountModel
{
    public int Id { get; set; }
    public int CostCategoryId { get; set; }
    public CostCategory? CostCategory { get; set; }
}
