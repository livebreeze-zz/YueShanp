using System;
using System.Collections.Generic;

namespace YueShanp.Models
{
    public class PrePrintDeliveryOrderViewModel
    {
        public string CustomerName { get; set; }
        public DateTime DeliveryOrderDate { get; set; }
        public List<DeliveryOrderDetail> DeliveryOrderDetailList { get; set; }
        public decimal TotalAmount { get; set; }
    }
}