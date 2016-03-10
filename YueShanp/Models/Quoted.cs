using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class Quoted : BaseEntity<int>
    {
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [DisplayName("產品報價")]
        public decimal QuotedPrice { get; set; }

        [DisplayName("備註")]
        public string Remark { get; set; }
    }
}