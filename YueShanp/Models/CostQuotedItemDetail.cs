using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class CostQuotedItemDetail : BaseEntity<int>
    {
        public CostQuotedItem Item { get; set; }

        [Required]
        [DisplayName("材料單價")]
        public decimal UnitPrice { get; set; }

        [Required]
        [DisplayName("材料用量")]
        public int Qty { get; set; }
    }
}