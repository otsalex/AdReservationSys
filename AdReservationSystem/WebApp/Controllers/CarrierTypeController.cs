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
    public class CarrierTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrierTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarrierType
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarrierTypes.ToListAsync());
        }

        // GET: CarrierType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrierType = await _context.CarrierTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrierType == null)
            {
                return NotFound();
            }

            return View(carrierType);
        }

        // GET: CarrierType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarrierType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] CarrierType carrierType)
        {
            if (ModelState.IsValid)
            {
                carrierType.Id = Guid.NewGuid();
                _context.Add(carrierType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrierType);
        }

        // GET: CarrierType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrierType = await _context.CarrierTypes.FindAsync(id);
            if (carrierType == null)
            {
                return NotFound();
            }
            return View(carrierType);
        }

        // POST: CarrierType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type")] CarrierType carrierType)
        {
            if (id != carrierType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrierType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrierTypeExists(carrierType.Id))
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
            return View(carrierType);
        }

        // GET: CarrierType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrierType = await _context.CarrierTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrierType == null)
            {
                return NotFound();
            }

            return View(carrierType);
        }

        // POST: CarrierType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carrierType = await _context.CarrierTypes.FindAsync(id);
            if (carrierType != null)
            {
                _context.CarrierTypes.Remove(carrierType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrierTypeExists(Guid id)
        {
            return _context.CarrierTypes.Any(e => e.Id == id);
        }
    }
}
#pragma warning restore 1591