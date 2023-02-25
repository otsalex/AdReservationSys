using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class AdSpaceInReservation
{
    public Guid AdSpaceInReservationId { get; set; }

    [Required] 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    [Required]
    [ForeignKey("ReservationId")]
    public Guid ReservationId { get; set; }
    
    [Required]
    [ForeignKey("AdDesignId")]
    public Guid AdDesignId { get; set; }
    
    [Required]
    [ForeignKey("AdSpaceId")]
    public Guid AdSpaceId { get; set; }
    
    
    public required Reservation Reservation { get; set; }
    public required AdDesign AdDesign { get; set; }
    public required AdSpace AdSpace { get; set; }
}