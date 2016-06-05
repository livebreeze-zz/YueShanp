using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class DeliveryOrderDetail : BaseEntity<int>
    {
        [Required]
        [DisplayName("數量")]
        public int Qty { get; set; }

        public virtual Product Product { get; set; }
    }
}