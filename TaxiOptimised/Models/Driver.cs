using System;
using System.Collections.Generic;

#nullable disable

namespace TaxiOptimised.Models
{
    public partial class Driver
    {
        public Driver()
        {
            DriverOrders = new HashSet<DriverOrder>();
        }

        public int DriverId { get; set; }
        public string Name { get; set; }
        public bool IsFree { get; set; }

        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
    }
}
