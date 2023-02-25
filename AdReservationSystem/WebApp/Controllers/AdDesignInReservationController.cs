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
    public class AdDesignInReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdDesignInReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdDesignInReservation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdDesignInReservations.Include(a => a.AdDesign).Include(a => a.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdDesignInReservation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesignInReservation = await _context.AdDesignInReservations
                .Include(a => a.AdDesign)
                .Include(a => a.Reservation)
                .FirstOrDefaultAsync(m => m.AdDesignInReservationId == id);
            if (adDesignInReservation == null)
            {
                return NotFound();
            }

            return View(adDesignInReservation);
        }

        // GET: AdDesignInReservation/Create
        public IActionResult Create()
        {
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name");
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName");
            return View();
        }

        // POST: AdDesignInReservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdDesignInReservationId,AdDesignId,ReservationId")] AdDesignInReservation adDesignInReservation)
        {
            if (ModelState.IsValid)
            {
                adDesignInReservation.AdDesignInReservationId = Guid.NewGuid();
                _context.Add(adDesignInReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name", adDesignInReservation.AdDesignId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName", adDesignInReservation.ReservationId);
            return View(adDesignInReservation);
        }

        // GET: AdDesignInReservation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesignInReservation = await _context.AdDesignInReservations.FindAsync(id);
            if (adDesignInReservation == null)
            {
                return NotFound();
            }
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name", adDesignInReservation.AdDesignId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName", adDesignInReservation.ReservationId);
            return View(adDesignInReservation);
        }

        // POST: AdDesignInReservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdDesignInReservationId,AdDesignId,ReservationId")] AdDesignInReservation adDesignInReservation)
        {
            if (id != adDesignInReservation.AdDesignInReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adDesignInReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdDesignInReservationExists(adDesignInReservation.AdDesignInReservationId))
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
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name", adDesignInReservation.AdDesignId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName", adDesignInReservation.ReservationId);
            return View(adDesignInReservation);
        }

        // GET: AdDesignInReservation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adDesignInReservation = await _context.AdDesignInReservations
                .Include(a => a.AdDesign)
                .Include(a => a.Reservation)
                .FirstOrDefaultAsync(m => m.AdDesignInReservationId == id);
            if (adDesignInReservation == null)
            {
                return NotFound();
            }

            return View(adDesignInReservation);
        }

        // POST: AdDesignInReservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adDesignInReservation = await _context.AdDesignInReservations.FindAsync(id);
            if (adDesignInReservation != null)
            {
                _context.AdDesignInReservations.Remove(adDesignInReservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdDesignInReservationExists(Guid id)
        {
            return _context.AdDesignInReservations.Any(e => e.AdDesignInReservationId == id);
        }
    }
}
