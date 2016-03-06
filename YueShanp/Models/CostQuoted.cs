using System.Collections.Generic;

namespace YueShanp.Models
{
    public class CostQuoted : Quoted
    {
        public List<CostQuotedItemDetail> ItemDetails { get; set; }
    }
}