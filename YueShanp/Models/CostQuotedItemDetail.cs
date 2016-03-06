namespace YueShanp.Models
{
    public class CostQuotedItemDetail : BaseEntity<int>
    {
        public CostQuotedItem Item { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
    }
}