namespace CostRegisterAppNET8.Data;

public class Cost : BaseTotalModel
{
    public int Id { get; set; }
    public int CostCategoryId { get; set; }
    public CostCategory CostCategory { get; set; } = null!;
}
