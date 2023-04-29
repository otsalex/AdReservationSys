using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class AdDesignInReservation : DomainEntityId
{
    [Required]
    [ForeignKey("AdDesignId")]
    public Guid AdDesignId { get; set; }
    
    [Required]
    [ForeignKey("ReservationId")]
    public Guid ReservationId { get; set; }
   
    public  AdDesign? AdDesign { get; set; }
    public  Reservation? Reservation { get; set; }
}