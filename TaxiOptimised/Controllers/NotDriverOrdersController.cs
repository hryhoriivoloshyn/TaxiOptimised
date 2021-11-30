using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxiOptimised;
using TaxiOptimised.Models;

namespace TaxiOptimised.Controllers
{
    public class NotDriverOrdersController : Controller
    {
        private readonly TaxiOptDBContext _context;

        public NotDriverOrdersController(TaxiOptDBContext context)
        {
            _context = context;
        }

        // GET: NotDriverOrders
        public async Task<IActionResult> Index()
        {
            var taxiOptDBContext = _context.DriverOrders.Include(d => d.Driver).Include(d => d.Order);
            return View(await taxiOptDBContext.ToListAsync());
        }

        // GET: NotDriverOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverOrder = await _context.DriverOrders
                .Include(d => d.Driver)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverOrder == null)
            {
                return NotFound();
            }

            return View(driverOrder);
        }

        // GET: NotDriverOrders/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: NotDriverOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,OrderId,DistanceToDriver,IsDesignated")] DriverOrder driverOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", driverOrder.DriverId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", driverOrder.OrderId);
            return View(driverOrder);
        }

        // GET: NotDriverOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverOrder = await _context.DriverOrders.FindAsync(id);
            if (driverOrder == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", driverOrder.DriverId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", driverOrder.OrderId);
            return View(driverOrder);
        }

        // POST: NotDriverOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriverId,OrderId,DistanceToDriver,IsDesignated")] DriverOrder driverOrder)
        {
            if (id != driverOrder.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverOrderExists(driverOrder.DriverId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", driverOrder.DriverId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", driverOrder.OrderId);
            return View(driverOrder);
        }

        // GET: NotDriverOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverOrder = await _context.DriverOrders
                .Include(d => d.Driver)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverOrder == null)
            {
                return NotFound();
            }

            return View(driverOrder);
        }

        // POST: NotDriverOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverOrder = await _context.DriverOrders.FindAsync(id);
            _context.DriverOrders.Remove(driverOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverOrderExists(int id)
        {
            return _context.DriverOrders.Any(e => e.DriverId == id);
        }
    }
}
