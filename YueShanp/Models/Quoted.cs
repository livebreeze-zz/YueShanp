using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class Quoted : BaseEntity<int>
    {
        public Customer Customer { get; set; }
        public Product Product { get; set; }

        [Required]
        [DisplayName("產品報價")]
        public decimal QuotedPrice { get; set; }

        public string Remark { get; set; }
    }
}