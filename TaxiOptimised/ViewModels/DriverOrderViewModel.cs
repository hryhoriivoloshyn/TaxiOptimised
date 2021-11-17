using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiOptimised.Models;

namespace TaxiOptimised.ViewModels
{
    public class DriverOrderViewModel
    {
        public DriverOrder DriverOrder { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
