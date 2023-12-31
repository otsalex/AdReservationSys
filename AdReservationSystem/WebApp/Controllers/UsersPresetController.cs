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
    public class UsersPresetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersPresetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersPreset
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UsersPresets.Include(u => u.Preset);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UsersPreset/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersPreset = await _context.UsersPresets
                .Include(u => u.Preset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersPreset == null)
            {
                return NotFound();
            }

            return View(usersPreset);
        }

        // GET: UsersPreset/Create
        public IActionResult Create()
        {
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name");
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "UserName");
            return View();
        }

        // POST: UsersPreset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PresetId")] UsersPreset usersPreset)
        {
            if (ModelState.IsValid)
            {
                usersPreset.Id = Guid.NewGuid();
                _context.Add(usersPreset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "UserName", usersPreset.AppUserId);
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name", usersPreset.PresetId);
            return View(usersPreset);
        }

        // GET: UsersPreset/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersPreset = await _context.UsersPresets.FindAsync(id);
            if (usersPreset == null)
            {
                return NotFound();
            }
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name", usersPreset.PresetId);
            return View(usersPreset);
        }

        // POST: UsersPreset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PresetId")] UsersPreset usersPreset)
        {
            if (id != usersPreset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersPreset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersPresetExists(usersPreset.Id))
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
            ViewData["PresetId"] = new SelectList(_context.Presets, "PresetId", "Name", usersPreset.PresetId);
            return View(usersPreset);
        }

        // GET: UsersPreset/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersPreset = await _context.UsersPresets
                .Include(u => u.Preset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersPreset == null)
            {
                return NotFound();
            }

            return View(usersPreset);
        }

        // POST: UsersPreset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usersPreset = await _context.UsersPresets.FindAsync(id);
            if (usersPreset != null)
            {
                _context.UsersPresets.Remove(usersPreset);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersPresetExists(Guid id)
        {
            return _context.UsersPresets.Any(e => e.Id == id);
        }
    }
}
#pragma warning restore 1591
