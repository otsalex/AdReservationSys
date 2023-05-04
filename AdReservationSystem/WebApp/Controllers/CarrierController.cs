#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Domain.App;

namespace WebApp.Controllers
{
    public class CarrierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carrier
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Carriers.Include(c => c.CarrierType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Carrier/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrier = await _context.Carriers
                .Include(c => c.CarrierType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrier == null)
            {
                return NotFound();
            }

            return View(carrier);
        }

        // GET: Carrier/Create
        public IActionResult Create()
        {
            ViewData["CarrierTypeId"] = new SelectList(_context.CarrierTypes, "CarrierTypeId", "Type");
            return View();
        }

        // POST: Carrier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Number,GPSX,GPSY,BusStopName,Street,Direction,CarrierTypeId")] Carrier carrier)
        {
            if (ModelState.IsValid)
            {
                carrier.Id = Guid.NewGuid();
                _context.Add(carrier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarrierTypeId"] = new SelectList(_context.CarrierTypes, "CarrierTypeId", "Type", carrier.CarrierTypeId);
            return View(carrier);
        }

        // GET: Carrier/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier == null)
            {
                return NotFound();
            }
            ViewData["CarrierTypeId"] = new SelectList(_context.CarrierTypes, "CarrierTypeId", "Type", carrier.CarrierTypeId);
            return View(carrier);
        }

        // POST: Carrier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,City,Number,GPSX,GPSY,BusStopName,Street,Direction,CarrierTypeId")] Carrier carrier)
        {
            if (id != carrier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrierExists(carrier.Id))
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
            ViewData["CarrierTypeId"] = new SelectList(_context.CarrierTypes, "CarrierTypeId", "Type", carrier.CarrierTypeId);
            return View(carrier);
        }

        // GET: Carrier/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrier = await _context.Carriers
                .Include(c => c.CarrierType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrier == null)
            {
                return NotFound();
            }

            return View(carrier);
        }

        // POST: Carrier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier != null)
            {
                _context.Carriers.Remove(carrier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrierExists(Guid id)
        {
            return _context.Carriers.Any(e => e.Id == id);
        }
    }
}
#pragma warning restore 1591