using System.Collections.Generic;

namespace YueShanp.Models
{
    public class ProductCostViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<CostItem> CostItems { get; set; }
    }
}