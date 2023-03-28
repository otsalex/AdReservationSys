using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.App;

namespace WebApp.Controllers
{
    public class AdSpaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdSpaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdSpace
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdSpaces.Include(a => a.AdSpaceType).Include(a => a.Carrier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdSpace/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpace = await _context.AdSpaces
                .Include(a => a.AdSpaceType)
                .Include(a => a.Carrier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adSpace == null)
            {
                return NotFound();
            }

            return View(adSpace);
        }

        // GET: AdSpace/Create
        public IActionResult Create()
        {
            ViewData["AdSpaceTypeId"] = new SelectList(_context.AdSpaceTypes, "AdSpaceTypeId", "Type");
            ViewData["CarrierId"] = new SelectList(_context.Carriers, "CarrierId", "City");
            return View();
        }

        // POST: AdSpace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdSpaceId,Side,RefToImage,AdSpaceTypeId,CarrierId")] AdSpace adSpace)
        {
            if (ModelState.IsValid)
            {
                adSpace.Id = Guid.NewGuid();
                _context.Add(adSpace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdSpaceTypeId"] = new SelectList(_context.AdSpaceTypes, "AdSpaceTypeId", "Type", adSpace.AdSpaceTypeId);
            ViewData["CarrierId"] = new SelectList(_context.Carriers, "CarrierId", "City", adSpace.CarrierId);
            return View(adSpace);
        }

        // GET: AdSpace/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpace = await _context.AdSpaces.FindAsync(id);
            if (adSpace == null)
            {
                return NotFound();
            }
            ViewData["AdSpaceTypeId"] = new SelectList(_context.AdSpaceTypes, "AdSpaceTypeId", "Type", adSpace.AdSpaceTypeId);
            ViewData["CarrierId"] = new SelectList(_context.Carriers, "CarrierId", "City", adSpace.CarrierId);
            return View(adSpace);
        }

        // POST: AdSpace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdSpaceId,Side,RefToImage,AdSpaceTypeId,CarrierId")] AdSpace adSpace)
        {
            if (id != adSpace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adSpace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdSpaceExists(adSpace.Id))
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
            ViewData["AdSpaceTypeId"] = new SelectList(_context.AdSpaceTypes, "AdSpaceTypeId", "Type", adSpace.AdSpaceTypeId);
            ViewData["CarrierId"] = new SelectList(_context.Carriers, "CarrierId", "City", adSpace.CarrierId);
            return View(adSpace);
        }

        // GET: AdSpace/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpace = await _context.AdSpaces
                .Include(a => a.AdSpaceType)
                .Include(a => a.Carrier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adSpace == null)
            {
                return NotFound();
            }

            return View(adSpace);
        }

        // POST: AdSpace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adSpace = await _context.AdSpaces.FindAsync(id);
            if (adSpace != null)
            {
                _context.AdSpaces.Remove(adSpace);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdSpaceExists(Guid id)
        {
            return _context.AdSpaces.Any(e => e.Id == id);
        }
    }
}
