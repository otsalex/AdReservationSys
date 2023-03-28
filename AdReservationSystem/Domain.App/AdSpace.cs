using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class AdSpace : DomainEntityId
{
    [Required]
    public required string Side { get; set; }
    [MaxLength(255)]
    public required string RefToImage { get; set; }
    
    [Required]
    [ForeignKey("AdSpaceTypeId")]
    public Guid AdSpaceTypeId { get; set; }
    
    [Required]
    [ForeignKey("CarrierId")]
    public Guid CarrierId { get; set; }
    
    public  AdSpaceType? AdSpaceType { get; set; }
    public  Carrier? Carrier { get; set; }
    
    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdSpaceInPreset>? AdSpaceInPresets { get; set; }
    public ICollection<AdSpacePrice>? AdSpacePrices { get; set; }
}