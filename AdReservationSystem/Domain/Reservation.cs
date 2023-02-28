using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Reservation
{
    public Guid ReservationId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string CampaignName { get; set; }
    
    [Required]
    public required string State { get; set; }
    
    [Required]
    public DateTime CreationTime { get; set; }
    
    public DateTime? ApprovalTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    
    [Required]
    [ForeignKey("AppUserId")]
    public Guid AppUserId { get; set; }
    
    public AppUser? User { get; set; }

    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdDesignInReservation>? AdDesignInReservations { get; set; }
}