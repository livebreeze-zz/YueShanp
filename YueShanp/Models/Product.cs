using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [DisplayName("名稱")]
        public string Name { get; set; }

        [Required]
        [DisplayName("產品報價")]
        public decimal QuotedPrice { get; set; }

        [DisplayName("備註")]
        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<CostItem> CostItems { get; set; }
    }
}