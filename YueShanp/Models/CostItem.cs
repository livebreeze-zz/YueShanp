using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class CostItem : BaseEntity<int>
    {
        [Required]
        [DisplayName("材料名稱")]
        public string Name { get; set; }

        [Required]
        [DisplayName("材料單價")]
        public decimal UnitPrice { get; set; }

        [Required]
        [DisplayName("材料需要數量")]
        public int ItemQty { get; set; }

        [Required]
        [DisplayName("材料種類")]
        public ItemType ItemType { get; set; }

        public virtual Product Product { get; set; }
    }
}