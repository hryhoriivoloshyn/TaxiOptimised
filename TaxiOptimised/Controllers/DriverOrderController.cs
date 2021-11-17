using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiOptimised.Models;
using TaxiOptimised.ViewModels;

namespace TaxiOptimised.Controllers
{
    public class DriverOrderController : Controller
    {
        private TaxiOptDBContext db;
        private DriverOrderViewModel viewModel;
        public DriverOrderController(TaxiOptDBContext context)
        {
            db = context;
            viewModel = new DriverOrderViewModel()
            {
                Drivers = db.Drivers,
                Orders = db.Orders
            };
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DriverOrder DriverOrder)
        {
            db.DriverOrders.Add(DriverOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? driverId, int? orderId)
        {
            if (driverId != null&& orderId!=null)
            {
                DriverOrder DriverOrder = await db.DriverOrders.FirstOrDefaultAsync(p => p.DriverId == driverId&&p.OrderId==orderId);
                DriverOrder.Driver=await  db.Drivers.FirstOrDefaultAsync(p => p.DriverId == driverId);
                DriverOrder.Order = await db.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);

                if (DriverOrder != null)
                {
                    return View(DriverOrder);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DriverOrder DriverOrder)
        {
            
            db.DriverOrders.Update(DriverOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? driverId, int? orderId)
        {
            if (driverId != null && orderId != null)
            {
                DriverOrder DriverOrder = await db.DriverOrders.FirstOrDefaultAsync(p => p.DriverId == driverId && p.OrderId == orderId);
                if (DriverOrder != null)
                {
                    db.DriverOrders.Remove(DriverOrder);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            return NotFound();
        }

    }
}
