using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Reservation
{
    public Guid ReservationId { get; set; }
    
    [Required]
    public required string CampaignName { get; set; }
    
    [Required]
    public required string State { get; set; }
    
    [Required]
    public DateTime CreationTime { get; set; }
    
    public DateTime? ApprovalTime { get; set; }
    public DateTime? EndTime { get; set; }
    

    public virtual IdentityUser? User { get; set; }

    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdDesignInReservation>? AdDesignInReservations { get; set; }
}