using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demogsoft1.Models
{
    public class Combinedpurchase
    {
        public int purkey { get; set; }
        public trnPurchase trnPurchase { get; set; }
        public trnPurchaseItem item { get; set; }
    }
}