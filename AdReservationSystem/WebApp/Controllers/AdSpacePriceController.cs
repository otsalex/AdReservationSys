using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Controllers
{
    public class AdSpacePriceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdSpacePriceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdSpacePrice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdSpacePrices.Include(a => a.AdSpace);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdSpacePrice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpacePrice = await _context.AdSpacePrices
                .Include(a => a.AdSpace)
                .FirstOrDefaultAsync(m => m.AdSpacePriceId == id);
            if (adSpacePrice == null)
            {
                return NotFound();
            }

            return View(adSpacePrice);
        }

        // GET: AdSpacePrice/Create
        public IActionResult Create()
        {
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage");
            return View();
        }

        // POST: AdSpacePrice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdSpacePriceId,Price,StartTime,EndTime,AdSpaceId")] AdSpacePrice adSpacePrice)
        {
            if (ModelState.IsValid)
            {
                adSpacePrice.AdSpacePriceId = Guid.NewGuid();
                _context.Add(adSpacePrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpacePrice.AdSpaceId);
            return View(adSpacePrice);
        }

        // GET: AdSpacePrice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpacePrice = await _context.AdSpacePrices.FindAsync(id);
            if (adSpacePrice == null)
            {
                return NotFound();
            }
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpacePrice.AdSpaceId);
            return View(adSpacePrice);
        }

        // POST: AdSpacePrice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdSpacePriceId,Price,StartTime,EndTime,AdSpaceId")] AdSpacePrice adSpacePrice)
        {
            if (id != adSpacePrice.AdSpacePriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adSpacePrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdSpacePriceExists(adSpacePrice.AdSpacePriceId))
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
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpacePrice.AdSpaceId);
            return View(adSpacePrice);
        }

        // GET: AdSpacePrice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpacePrice = await _context.AdSpacePrices
                .Include(a => a.AdSpace)
                .FirstOrDefaultAsync(m => m.AdSpacePriceId == id);
            if (adSpacePrice == null)
            {
                return NotFound();
            }

            return View(adSpacePrice);
        }

        // POST: AdSpacePrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adSpacePrice = await _context.AdSpacePrices.FindAsync(id);
            if (adSpacePrice != null)
            {
                _context.AdSpacePrices.Remove(adSpacePrice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdSpacePriceExists(Guid id)
        {
            return _context.AdSpacePrices.Any(e => e.AdSpacePriceId == id);
        }
    }
}
