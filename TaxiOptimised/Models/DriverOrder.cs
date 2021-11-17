using System;
using System.Collections.Generic;

#nullable disable

namespace TaxiOptimised.Models
{
    public partial class DriverOrder
    {
        public int DriverId { get; set; }
        public int OrderId { get; set; }
        public double DistanceToDriver { get; set; }
        public bool IsDesignated { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Order Order { get; set; }
    }
}
