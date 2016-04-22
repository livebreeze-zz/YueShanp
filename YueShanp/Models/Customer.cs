using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class Customer : BaseEntity<int>
    {
        [Required]
        [DisplayName("公司名稱")]
        public string Name { get; set; }

        [DisplayName("公司全名")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("聯絡電話")]
        [UIHint("PhoneNumber")]
        public string Phone { get; set; }

        [DisplayName("傳真號碼")]
        public string Fax { get; set; }

        [DisplayName("公司地址")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("採購姓名")]
        public string Purchaser { get; set; }

        [DisplayName("統一編號")]
        public string TIN { get; set; }
    }
}