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
        public int DeliveryOrderNumber { get; set; }

        [DisplayName("客戶訂單編號")]
        public string CustomerSONumber { get; set; }

        [Required]
        [DisplayName("出貨日期")]
        public DateTime DeliveryOrderDate { get; set; }

        [Required]
        [StringLength(6)]
        [DisplayName("帳款月份")]
        public string ReceivableMonth { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual List<DeliveryOrderDetail> DeliveryOrderDetailList { get; set; }

        public decimal TotalAmount { get; set; }

    }
}