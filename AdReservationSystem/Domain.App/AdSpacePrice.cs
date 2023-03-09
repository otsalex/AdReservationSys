﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App;

public class AdSpacePrice
{
    public Guid AdSpacePriceId { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    [Required]
    [ForeignKey("AdSpaceId")]
    public Guid AdSpaceId { get; set; }
    
    public  AdSpace? AdSpace { get; set; }
}