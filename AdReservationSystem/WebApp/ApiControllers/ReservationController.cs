using DAL;
using DAL.Contacts.App;
using Domain.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReservationController : ControllerBase
{

    private readonly ApplicationDbContext _context;
    private readonly IAppUOW _uow;

    public ReservationController(IAppUOW uow, ApplicationDbContext context)
    {
        _uow = uow;
        _context = context;
    }

    // GET: api/Reservations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
    {
        var reservations = await _uow.ReservationRepository.AllAsync(User.GetUserId());
        return Ok(reservations);
    }
    
    // GET: api/Reservations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Reservation>> GetReservation(Guid id)
    {
        var reservation = await _uow.ReservationRepository.FindAsync(id, User.GetUserId());
        
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }
    
    // PUT: api/Reservations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult> PutReservation(Guid id, Reservation reservation)
    {
        if (id != reservation.Id)
        {
            return BadRequest();
        }

        _context.Entry(reservation).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReservationExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    // POST: api/Reservations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
    {
        reservation.AppUserId = User.GetUserId();
        _context.Reservations.Add(reservation);
        _uow.ReservationRepository.Add(reservation);

        await _uow.SaveChangesAsync();
        return CreatedAtAction("GetReservation", new { id = reservation.Id, reservation });
    }
    // DELETE: api/Reservations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        if (_context.Reservations  == null)
        {
            return NotFound();
        }
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReservationExists(Guid id)
    {
        return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}