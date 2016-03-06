namespace YueShanp.Models
{
    public class SimpleQuoted : BaseEntity<int>
    {
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public decimal QuotedPrice { get; set; }
        public string Remark { get; set; }
    }
}