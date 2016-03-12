using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class ProductCost : BaseEntity<int>
    {
        [Required]
        [DisplayName("項目數量")]
        public int ItemQty { get; set; }

        public virtual Item Item { get; set; }        
    }
}