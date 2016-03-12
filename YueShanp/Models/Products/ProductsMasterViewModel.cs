using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YueShanp.Models
{
    public class ProductsMasterViewModel
    {
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}