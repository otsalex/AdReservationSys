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
    public class AdSpaceTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdSpaceTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdSpaceType
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdSpaceTypes.ToListAsync());
        }

        // GET: AdSpaceType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceType = await _context.AdSpaceTypes
                .FirstOrDefaultAsync(m => m.AdSpaceTypeId == id);
            if (adSpaceType == null)
            {
                return NotFound();
            }

            return View(adSpaceType);
        }

        // GET: AdSpaceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdSpaceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdSpaceTypeId,Type,Height,Width,Material,Description")] AdSpaceType adSpaceType)
        {
            if (ModelState.IsValid)
            {
                adSpaceType.AdSpaceTypeId = Guid.NewGuid();
                _context.Add(adSpaceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adSpaceType);
        }

        // GET: AdSpaceType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceType = await _context.AdSpaceTypes.FindAsync(id);
            if (adSpaceType == null)
            {
                return NotFound();
            }
            return View(adSpaceType);
        }

        // POST: AdSpaceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdSpaceTypeId,Type,Height,Width,Material,Description")] AdSpaceType adSpaceType)
        {
            if (id != adSpaceType.AdSpaceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adSpaceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdSpaceTypeExists(adSpaceType.AdSpaceTypeId))
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
            return View(adSpaceType);
        }

        // GET: AdSpaceType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceType = await _context.AdSpaceTypes
                .FirstOrDefaultAsync(m => m.AdSpaceTypeId == id);
            if (adSpaceType == null)
            {
                return NotFound();
            }

            return View(adSpaceType);
        }

        // POST: AdSpaceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adSpaceType = await _context.AdSpaceTypes.FindAsync(id);
            if (adSpaceType != null)
            {
                _context.AdSpaceTypes.Remove(adSpaceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdSpaceTypeExists(Guid id)
        {
            return _context.AdSpaceTypes.Any(e => e.AdSpaceTypeId == id);
        }
    }
}
