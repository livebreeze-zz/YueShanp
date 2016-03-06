namespace YueShanp.Models
{
    public class CostQuotedItem : BaseEntity<int>
    {
        public string Name { get; set; }
        public ItemType ItemType { get; set; }
    }
}