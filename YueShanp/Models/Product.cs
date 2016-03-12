using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class Product :BaseEntity<int>
    {
        [Required]
        [DisplayName("名稱")]
        public string Name { get; set; }
    }
}