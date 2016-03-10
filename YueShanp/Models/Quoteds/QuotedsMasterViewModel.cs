using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YueShanp.Models
{
    public class QuotedsMasterViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Quoted> QuotedList { get; set; }
    }
}