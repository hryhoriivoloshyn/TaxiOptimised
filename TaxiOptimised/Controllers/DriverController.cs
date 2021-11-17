using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaxiOptimised.Models;

namespace TaxiOptimised.Controllers
{
    public class DriverController : Controller
    {
        private TaxiOptDBContext db;

        public DriverController(TaxiOptDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Driver driver)
        {
            db.Drivers.Add(driver);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public async  Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Driver driver = await db.Drivers.FirstOrDefaultAsync(p => p.DriverId == id);
                if (driver != null)
                {
                    return View(driver);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Driver driver)
        {
            db.Drivers.Update(driver);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

     
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                Driver driver = await db.Drivers.FirstOrDefaultAsync(p => p.DriverId == id);
                if (driver != null)
                {
                    db.Drivers.Remove(driver);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            return NotFound();
        }
    }
}
