using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxiOptimised.Models;
using TaxiOptimised.ViewModels;

namespace TaxiOptimised.Controllers
{
    public class GreedyAlgorithmController : Controller
    {
        private TaxiOptDBContext db;
        private HomeViewModel viewModel;

        public GreedyAlgorithmController(TaxiOptDBContext context)
        {
            db = context;
            viewModel = new HomeViewModel()
            {
                Drivers = db.Drivers.ToList(),
                Orders = db.Orders.ToList(),
                DriverOrders = db.DriverOrders.ToList()
            };
        }

        private async Task CalculateGreedyAlgorithm(HomeViewModel viewModel)
        {
            IEnumerable<Driver> drivers = viewModel.Drivers;
            IEnumerable<Order> orders = viewModel.Orders;
            IEnumerable<DriverOrder> driverOrders = viewModel.DriverOrders;

            double maxProfitRatio = 0;
            double profitRatio;
            foreach (var drOrders in driverOrders)
            {
                profitRatio = (1 / drOrders.DistanceToDriver + drOrders.Order.Distance);
                //if (drOrders.DistanceToDriver > minToDriver || drOrders.Order.Distance < maxOrderDistance)
                //{
                    

                //}
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            foreach (var driverOrder in viewModel.DriverOrders)
            {
                driverOrder.Driver = await db.Drivers.FirstOrDefaultAsync(d => d.DriverId == driverOrder.DriverId);
                driverOrder.Order = await db.Orders.FirstOrDefaultAsync(p => p.OrderId == driverOrder.OrderId);

            }
            return View(viewModel);
        }
    }
}
