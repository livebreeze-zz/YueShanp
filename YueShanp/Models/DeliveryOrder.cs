using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.Models
{
    public class DeliveryOrder : BaseEntity<int>
    {
        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [StringLength(6)]
        public int ReceivableMoth { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual List<DeliveryOrderDetail> DeliveryOrderDetailList { get; set; }

    }
}