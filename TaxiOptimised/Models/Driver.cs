using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        [JsonIgnore]

        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
    }
}
