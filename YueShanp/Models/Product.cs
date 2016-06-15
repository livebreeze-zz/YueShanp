using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YueShanp.Models
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [Index("ProductNameIndex", IsUnique = true)]
        [DisplayName("名稱")]
        public string Name { get; set; }

        [Required]
        [DisplayName("單價")]
        public decimal UnitPrice { get; set; }

        [DisplayName("備註")]
        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<CostItem> CostItems { get; set; }
    }
}