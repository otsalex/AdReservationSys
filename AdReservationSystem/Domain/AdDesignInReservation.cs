using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class AdDesignInReservation
{
    public Guid AdDesignInReservationId { get; set; }
    
    [Required]
    [ForeignKey("AdDesignId")]
    public Guid AdDesignId { get; set; }
    
    [Required]
    [ForeignKey("ReservationId")]
    public Guid ReservationId { get; set; }
   
    public required AdDesign AdDesign { get; set; }
    public required Reservation Reservation { get; set; }
}