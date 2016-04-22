using System.Collections.Generic;
using YueShanp.Models;

namespace YueShanp.ViewModels
{
    public class ProductCostViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<CostItem> CostItems { get; set; }
    }
}