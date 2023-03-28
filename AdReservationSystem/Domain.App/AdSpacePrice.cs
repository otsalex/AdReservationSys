using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class AdSpacePrice : DomainEntityId
{
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