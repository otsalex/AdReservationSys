using AutoMapper;
using DAL.Contacts.App;
using Domain.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;
using Reservation = Domain.App.Reservation;

namespace WebApp.ApiControllers;

/// <summary>
/// API controller for Reservations
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReservationController : ControllerBase
{

    private readonly ReservationMapper _reservationMapper;
    private readonly AdSpaceMapper _adSpaceMapper;
    private readonly CarrierMapper _carrierMapper;
    private readonly IAppUOW _uow;

    /// <summary>
    /// Constructs a new ReservationController instance
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="mapper"></param>
    public ReservationController(IAppUOW uow, IMapper mapper)
    {
        _uow = uow;
        _reservationMapper = new ReservationMapper(mapper);
        _adSpaceMapper = new AdSpaceMapper(mapper);
        _carrierMapper = new CarrierMapper(mapper);
    }

    // GET: api/Reservations
    /// <summary>
    /// Gets all Reservations
    /// </summary>
    /// <returns>List of Reservations</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.ReservationWithAdSpaces>>> GetReservations()
    {
        var reservations = await _uow.ReservationRepository.AllAsync(User.GetUserId());
        var res = reservations
            .Select(e => _reservationMapper.MapWithAdSpaces(e))
            .ToList();
        return res!;
    }
    
    // GET: api/Reservations
    /// <summary>
    /// Gets all Reservations without AdSpaces
    /// </summary>
    /// <returns>List of reservations without AdSpaces</returns>
    [HttpGet("wo")]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.ReservationWOAdSpaces>>> GetReservationsWoAdSpaces()
    {
        var reservations = await _uow.ReservationRepository.AllAsync(User.GetUserId());
        var res = reservations
            .Select(e => _reservationMapper.MapWOAdSpaces(e))
            .ToList();
        return res!;
    }
    
    // GET: api/Reservations/5
    /// <summary>
    /// Gets a Reservation by the ID
    /// </summary>
    /// <param name="id">Reservation ID (Guid)</param>
    /// <returns>Reservation</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Public.DTO.v1.ReservationWithAdSpaces>> GetReservation(Guid id)
    {
        var reservation = await _uow.ReservationRepository.FindAsync(id, User.GetUserId());
        
        if (reservation == null)
        {
            return NotFound();
        }

        var res = _reservationMapper.MapWithAdSpaces(reservation);
        if (res != null)
        {
            res.AdSpaces = new List<AdSpaceMin>();
            foreach (var rel in reservation.AdSpaceInReservations)
            {
                var adSpace = _adSpaceMapper.Map(rel.AdSpace);
                res.AdSpaces.Add(adSpace);
            }
        }
        return res!;
    }
    // GET: api/Reservations/wo/5
    /// <summary>
    /// Gets a Reservation by the ID
    /// </summary>
    /// <param name="id">Reservation ID (Guid)</param>
    /// <returns>Reservation without AdSpaces</returns>
    [HttpGet("{id}/wo")]
    public async Task<ActionResult<Public.DTO.v1.ReservationWOAdSpaces>> GetReservationWoAdSpaces(Guid id)
    {
        var reservation = await _uow.ReservationRepository.FindAsync(id, User.GetUserId());
        
        if (reservation == null)
        {
            return NotFound();
        }

        var res = _reservationMapper.MapWOAdSpaces(reservation);
        return res!;
    }
    
    // PUT: api/Reservations/5
    /// <summary>
    /// Updates a Reservation in database
    /// </summary>
    /// <param name="id">Reservation ID (Guid)</param>
    /// <param name="reservation">Reservation instance</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> PutReservation(Guid id, ReservationWithAdSpaces reservation)
    {
        if (id != reservation.Id)
        {
            return BadRequest();
        }
        if (!await _uow.ReservationRepository.IsOwnedByUserAsync(reservation.Id, User.GetUserId()))
        {
            return BadRequest("No hacking (bad user id)!");
        }

        var existingReservation = await _uow.ReservationRepository.FindAsync(id);
        if (existingReservation == null) return NotFound();

        existingReservation.AppUserId = User.GetUserId();
        existingReservation.StartDate = reservation.StartDate.Date;
        existingReservation.EndDate = reservation.EndDate.Date;
        existingReservation.State = "pending";
        existingReservation.CampaignName = reservation.CampaignName;
        existingReservation.City = reservation.City;
        existingReservation.AdSpaceInReservations = new List<AdSpaceInReservation>();
        
        foreach (var adSpace in reservation.AdSpaces)
        {
            existingReservation.AdSpaceInReservations.Add(new AdSpaceInReservation()
            {
                AdSpaceId = adSpace.Id,
                ReservationId = reservation.Id
            });
        }
        _uow.ReservationRepository.Update(existingReservation);
        
        await _uow.SaveChangesAsync();


        return CreatedAtAction("GetReservation", existingReservation.Id);
    }
    // POST: api/Reservations
    /// <summary>
    /// Adds a new Reservation to the database
    /// </summary>
    /// <param name="reservation">Reservation instance</param>
    /// <returns>201-created</returns>
    [HttpPost]
    public async Task<ActionResult<Reservation>> PostReservation([FromBody] ReservationWithAdSpaces reservation)
    {

        var res = new Reservation
        {
            AppUserId = User.GetUserId(),
            CreationTime = DateTime.Now,
            StartDate = reservation.StartDate.Date,
            EndDate = reservation.EndDate.Date,
            State = reservation.State,
            CampaignName = reservation.CampaignName,
            City = reservation.City,
            AdSpaceInReservations = new List<AdSpaceInReservation>()
        };
        foreach (var adSpace in reservation.AdSpaces)
        {
            res.AdSpaceInReservations.Add(new AdSpaceInReservation()
            {
                AdSpaceId = adSpace.Id,
                ReservationId = reservation.Id
            });
        }
        var saved = _uow.ReservationRepository.Add(res);

        await _uow.SaveChangesAsync();
        
        return CreatedAtAction("GetReservation", saved.Id);
    }
    // DELETE: api/Reservations/5
    /// <summary>
    /// Deletes the Reservation from the database
    /// </summary>
    /// <param name="id">Reservation ID (Guid)</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        var reservation = await _uow.ReservationRepository.RemoveAsync(id);
        if (reservation == null) return NotFound();
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}