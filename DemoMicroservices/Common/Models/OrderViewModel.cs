using System;

namespace Common.Models
{
    public class OrderViewModel
    {
        public double OrderAmount { get; set; }

        public string OrderNumber { get; set; }
        public string Status { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
