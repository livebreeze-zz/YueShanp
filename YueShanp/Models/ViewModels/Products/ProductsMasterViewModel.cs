using System.Collections.Generic;

namespace YueShanp.Models
{
    public class ProductsMasterViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}