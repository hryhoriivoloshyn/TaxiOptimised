using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiOptimised.Models;

namespace TaxiOptimised.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<DriverOrder> DriverOrders { get; set; }
    }
}
