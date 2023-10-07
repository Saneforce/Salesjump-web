using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bus_Objects
{
    public class masterClass
    {
        public class retailer
        {
            public string custCode { get; set; }
            public string custName { get; set; }
            public string custSFCode { get; set; }
            public string custClass { get; set; }
            public string FYear { get; set; }
            public string FMonth { get; set; }
            public decimal cAmount { get; set; }
            public string RoName { get; set; }
            
        }
        public class distributor
        {
            public string distCode { get; set; }
            public string distName { get; set; }
            public string distSFCode { get; set; }
            public string FYear { get; set; }
            public string FMonth { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
