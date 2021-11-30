using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#nullable disable

namespace TaxiOptimised.Models
{
    public partial class Order
    {
        public Order()
        {
            DriverOrders = new HashSet<DriverOrder>();
        }

        public int OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public double ?Distance { get; set; }

        [JsonIgnore]
        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
    }
}
