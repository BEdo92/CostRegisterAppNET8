namespace CostRegisterAppNET8.Data
{
    public abstract class BaseAmountModel
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public AppUser? AppUser { get; set; }
        public string? AppUserId { get; set; }
    }
}
