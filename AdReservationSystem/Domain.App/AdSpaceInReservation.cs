﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class AdSpaceInReservation : DomainEntityId
{
    [Required] 
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    [Required]
    [ForeignKey("ReservationId")]
    public Guid ReservationId { get; set; }
    
    
    [ForeignKey("AdDesignId")]
    public Guid? AdDesignId { get; set; }
    
    [Required]
    [ForeignKey("AdSpaceId")]
    public Guid AdSpaceId { get; set; }
    public  Reservation? Reservation { get; set; }
    
    public  AdDesign? AdDesign { get; set; }
    public  AdSpace? AdSpace { get; set; }
}