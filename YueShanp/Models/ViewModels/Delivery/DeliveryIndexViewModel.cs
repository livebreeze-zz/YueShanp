using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YueShanp.Models
{
    public class DeliveryIndexViewModel
    {
        public IQueryable<Customer> CustomerList { get; set; }

        public AddDelivery AddDelivery { get; set; }
    }
}