using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class ProductCost : BaseEntity<int>
    {
        

        public virtual CostItem Item { get; set; }        
    }
}