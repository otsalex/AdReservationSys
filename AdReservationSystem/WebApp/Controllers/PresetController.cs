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
    public class PresetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Preset
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Presets.Include(p => p.PresetType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Preset/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets
                .Include(p => p.PresetType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preset == null)
            {
                return NotFound();
            }

            return View(preset);
        }

        // GET: Preset/Create
        public IActionResult Create()
        {
            ViewData["PresetTypeId"] = new SelectList(_context.PresetTypes, "PresetTypeId", "Type");
            return View();
        }

        // POST: Preset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PresetTypeId")] Preset preset)
        {
            if (ModelState.IsValid)
            {
                preset.Id = Guid.NewGuid();
                _context.Add(preset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PresetTypeId"] = new SelectList(_context.PresetTypes, "PresetTypeId", "Type", preset.PresetTypeId);
            return View(preset);
        }

        // GET: Preset/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets.FindAsync(id);
            if (preset == null)
            {
                return NotFound();
            }
            ViewData["PresetTypeId"] = new SelectList(_context.PresetTypes, "PresetTypeId", "Type", preset.PresetTypeId);
            return View(preset);
        }

        // POST: Preset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,PresetTypeId")] Preset preset)
        {
            if (id != preset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresetExists(preset.Id))
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
            ViewData["PresetTypeId"] = new SelectList(_context.PresetTypes, "PresetTypeId", "Type", preset.PresetTypeId);
            return View(preset);
        }

        // GET: Preset/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets
                .Include(p => p.PresetType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preset == null)
            {
                return NotFound();
            }

            return View(preset);
        }

        // POST: Preset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var preset = await _context.Presets.FindAsync(id);
            if (preset != null)
            {
                _context.Presets.Remove(preset);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresetExists(Guid id)
        {
            return _context.Presets.Any(e => e.Id == id);
        }
    }
}
#pragma warning restore 1591
