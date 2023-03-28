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
    public class AdSpaceInReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdSpaceInReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdSpaceInReservation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdSpaceInReservations.Include(a => a.AdDesign).Include(a => a.AdSpace).Include(a => a.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdSpaceInReservation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceInReservation = await _context.AdSpaceInReservations
                .Include(a => a.AdDesign)
                .Include(a => a.AdSpace)
                .Include(a => a.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adSpaceInReservation == null)
            {
                return NotFound();
            }

            return View(adSpaceInReservation);
        }

        // GET: AdSpaceInReservation/Create
        public IActionResult Create()
        {
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name");
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage");
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName");
            return View();
        }

        // POST: AdSpaceInReservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,ReservationId,AdDesignId,AdSpaceId")] AdSpaceInReservation adSpaceInReservation)
        {
            if (ModelState.IsValid)
            {
                adSpaceInReservation.Id = Guid.NewGuid();
                _context.Add(adSpaceInReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name", adSpaceInReservation.AdDesignId);
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpaceInReservation.AdSpaceId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName", adSpaceInReservation.ReservationId);
            return View(adSpaceInReservation);
        }

        // GET: AdSpaceInReservation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceInReservation = await _context.AdSpaceInReservations.FindAsync(id);
            if (adSpaceInReservation == null)
            {
                return NotFound();
            }
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name", adSpaceInReservation.AdDesignId);
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpaceInReservation.AdSpaceId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName", adSpaceInReservation.ReservationId);
            return View(adSpaceInReservation);
        }

        // POST: AdSpaceInReservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StartTime,EndTime,ReservationId,AdDesignId,AdSpaceId")] AdSpaceInReservation adSpaceInReservation)
        {
            if (id != adSpaceInReservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adSpaceInReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdSpaceInReservationExists(adSpaceInReservation.Id))
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
            ViewData["AdDesignId"] = new SelectList(_context.AdDesigns, "AdDesignId", "Name", adSpaceInReservation.AdDesignId);
            ViewData["AdSpaceId"] = new SelectList(_context.AdSpaces, "AdSpaceId", "RefToImage", adSpaceInReservation.AdSpaceId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "CampaignName", adSpaceInReservation.ReservationId);
            return View(adSpaceInReservation);
        }

        // GET: AdSpaceInReservation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSpaceInReservation = await _context.AdSpaceInReservations
                .Include(a => a.AdDesign)
                .Include(a => a.AdSpace)
                .Include(a => a.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adSpaceInReservation == null)
            {
                return NotFound();
            }

            return View(adSpaceInReservation);
        }

        // POST: AdSpaceInReservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adSpaceInReservation = await _context.AdSpaceInReservations.FindAsync(id);
            if (adSpaceInReservation != null)
            {
                _context.AdSpaceInReservations.Remove(adSpaceInReservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdSpaceInReservationExists(Guid id)
        {
            return _context.AdSpaceInReservations.Any(e => e.Id == id);
        }
    }
}
