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
    public class PresetTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresetTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PresetType
        public async Task<IActionResult> Index()
        {
            return View(await _context.PresetTypes.ToListAsync());
        }

        // GET: PresetType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presetType = await _context.PresetTypes
                .FirstOrDefaultAsync(m => m.PresetTypeId == id);
            if (presetType == null)
            {
                return NotFound();
            }

            return View(presetType);
        }

        // GET: PresetType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PresetType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresetTypeId,Type")] PresetType presetType)
        {
            if (ModelState.IsValid)
            {
                presetType.PresetTypeId = Guid.NewGuid();
                _context.Add(presetType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(presetType);
        }

        // GET: PresetType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presetType = await _context.PresetTypes.FindAsync(id);
            if (presetType == null)
            {
                return NotFound();
            }
            return View(presetType);
        }

        // POST: PresetType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PresetTypeId,Type")] PresetType presetType)
        {
            if (id != presetType.PresetTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presetType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresetTypeExists(presetType.PresetTypeId))
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
            return View(presetType);
        }

        // GET: PresetType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presetType = await _context.PresetTypes
                .FirstOrDefaultAsync(m => m.PresetTypeId == id);
            if (presetType == null)
            {
                return NotFound();
            }

            return View(presetType);
        }

        // POST: PresetType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var presetType = await _context.PresetTypes.FindAsync(id);
            if (presetType != null)
            {
                _context.PresetTypes.Remove(presetType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresetTypeExists(Guid id)
        {
            return _context.PresetTypes.Any(e => e.PresetTypeId == id);
        }
    }
}
