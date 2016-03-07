using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class CostQuotedItem : BaseEntity<int>
    {
        [Required]
        [DisplayName("名稱")]
        public string Name { get; set; }


        public ItemType ItemType { get; set; }
    }
}