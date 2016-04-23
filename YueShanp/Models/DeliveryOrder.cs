using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class DeliveryOrder : BaseEntity<int>
    {
        [Required]
        [DisplayName("出貨單編號")]
        public int DONumber { get; set; }

        [DisplayName("客戶訂單編號")]
        public string CustomerSONumber { get; set; }

        [Required]
        [DisplayName("出貨日期")]
        public DateTime DODate { get; set; }

        [Required]
        [StringLength(6)]
        [DisplayName("帳款月份")]
        public int ReceivableMoth { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual List<DeliveryOrderDetail> DODetailList { get; set; }

    }
}