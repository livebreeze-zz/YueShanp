using System.Collections.Generic;
using YueShanp.Models;

namespace YueShanp.ViewModels
{
    public class ProductsMasterViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}