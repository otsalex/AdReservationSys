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
    public class AdDesignController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdDesignController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdDesign
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdDesigns.ToListAsync());
        }

        // GET: AdDesign/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesign = await _context.AdDesigns
                .FirstOrDefaultAsync(m => m.AdDesignId == id);
            if (adDesign == null)
            {
                return NotFound();
            }

            return View(adDesign);
        }

        // GET: AdDesign/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdDesign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdDesignId,Name,RefToImage")] AdDesign adDesign)
        {
            if (ModelState.IsValid)
            {
                adDesign.AdDesignId = Guid.NewGuid();
                _context.Add(adDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adDesign);
        }

        // GET: AdDesign/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesign = await _context.AdDesigns.FindAsync(id);
            if (adDesign == null)
            {
                return NotFound();
            }
            return View(adDesign);
        }

        // POST: AdDesign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdDesignId,Name,RefToImage")] AdDesign adDesign)
        {
            if (id != adDesign.AdDesignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdDesignExists(adDesign.AdDesignId))
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
            return View(adDesign);
        }

        // GET: AdDesign/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesign = await _context.AdDesigns
                .FirstOrDefaultAsync(m => m.AdDesignId == id);
            if (adDesign == null)
            {
                return NotFound();
            }

            return View(adDesign);
        }

        // POST: AdDesign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adDesign = await _context.AdDesigns.FindAsync(id);
            if (adDesign != null)
            {
                _context.AdDesigns.Remove(adDesign);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdDesignExists(Guid id)
        {
            return _context.AdDesigns.Any(e => e.AdDesignId == id);
        }
    }
}
