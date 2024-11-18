namespace CostRegisterAppNET8.Data;

public class CostPlan : BaseTotalModel
{
    public int Id { get; set; }
    public int CostCategoryId { get; set; }

    // TODO: Maybe it would be better to create a different list of categories for this.
    // (just for the potentially bigger costs)
    public CostCategory CostCategory { get; set; } = null!;
}
