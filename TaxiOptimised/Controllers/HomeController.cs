using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaxiOptimised.Models;
using TaxiOptimised.ViewModels;

namespace TaxiOptimised.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TaxiOptDBContext db;

        private HomeViewModel viewModel;
        
        

        public HomeController(TaxiOptDBContext context)
        {
            db = context;
            viewModel = new HomeViewModel
            {
                Drivers = db.Drivers.ToList(),
                Orders = db.Orders.ToList(),
                DriverOrders = db.DriverOrders.ToList()

            };
        }

        public  IActionResult Index()
        {
            //foreach (var driverOrder in viewModel.DriverOrders)
            //{
            //    driverOrder.Driver =  db.Drivers.FirstOrDefault(d => d.DriverId == driverOrder.DriverId);
            //    driverOrder.Order =  db.Orders.FirstOrDefault(p => p.OrderId == driverOrder.OrderId);

            //}

            return View(viewModel);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}