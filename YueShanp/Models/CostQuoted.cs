namespace YueShanp.Models
{
    public class CostQuoted : BaseEntity<int>
    {
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public decimal TotalQuotedPrice { get; set; }
        public CostQuotedItemDetail[] ItemDetails { get; set; }
    }
}