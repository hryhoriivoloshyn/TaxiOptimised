using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiOptimised.Models;

namespace TaxiOptimised.Controllers
{
    public class OrderController : Controller
    {
        private TaxiOptDBContext db;

        public OrderController(TaxiOptDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Order Order)
        {
            db.Orders.Add(Order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Order Order = await db.Orders.FirstOrDefaultAsync(p=>p.OrderId==id);
                if (Order != null)
                {
                    return View(Order);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order Order)
        {
            db.Orders.Update(Order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Order Order = await db.Orders.FirstOrDefaultAsync(p => p.OrderId == id);
                if (Order != null)
                {
                    db.Orders.Remove(Order);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            return NotFound();
        }

    }
}
