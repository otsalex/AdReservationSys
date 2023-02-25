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
    public class AdSpaceInPresetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdSpaceInPresetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdSpaceInPreset
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdSpaceInPresets.Include(a => a.AdSpace).Include(a => a.Preset);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdSpaceInPreset/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceInPreset = await _context.AdSpaceInPresets
                .Include(a => a.AdSpace)
                .Include(a => a.Preset)
                .FirstOrDefaultAsync(m => m.AdSpaceInPresetId == id);
            if (adSpaceInPreset == null)
            {
                return NotFound();
            }

            return View(adSpaceInPreset);
        }

        // GET: AdSpaceInPreset/Create
        public IActionResult Create()
        {
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage");
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name");
            return View();
        }

        // POST: AdSpaceInPreset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdSpaceInPresetId,AdSpaceId,PresetId")] AdSpaceInPreset adSpaceInPreset)
        {
            if (ModelState.IsValid)
            {
                adSpaceInPreset.AdSpaceInPresetId = Guid.NewGuid();
                _context.Add(adSpaceInPreset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpaceInPreset.AdSpaceId);
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name", adSpaceInPreset.PresetId);
            return View(adSpaceInPreset);
        }

        // GET: AdSpaceInPreset/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceInPreset = await _context.AdSpaceInPresets.FindAsync(id);
            if (adSpaceInPreset == null)
            {
                return NotFound();
            }
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpaceInPreset.AdSpaceId);
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name", adSpaceInPreset.PresetId);
            return View(adSpaceInPreset);
        }

        // POST: AdSpaceInPreset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdSpaceInPresetId,AdSpaceId,PresetId")] AdSpaceInPreset adSpaceInPreset)
        {
            if (id != adSpaceInPreset.AdSpaceInPresetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adSpaceInPreset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdSpaceInPresetExists(adSpaceInPreset.AdSpaceInPresetId))
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
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpaceInPreset.AdSpaceId);
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name", adSpaceInPreset.PresetId);
            return View(adSpaceInPreset);
        }

        // GET: AdSpaceInPreset/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceInPreset = await _context.AdSpaceInPresets
                .Include(a => a.AdSpace)
                .Include(a => a.Preset)
                .FirstOrDefaultAsync(m => m.AdSpaceInPresetId == id);
            if (adSpaceInPreset == null)
            {
                return NotFound();
            }

            return View(adSpaceInPreset);
        }

        // POST: AdSpaceInPreset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adSpaceInPreset = await _context.AdSpaceInPresets.FindAsync(id);
            if (adSpaceInPreset != null)
            {
                _context.AdSpaceInPresets.Remove(adSpaceInPreset);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdSpaceInPresetExists(Guid id)
        {
            return _context.AdSpaceInPresets.Any(e => e.AdSpaceInPresetId == id);
        }
    }
}
