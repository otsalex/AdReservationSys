using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class AdSpace
{
    public Guid AdSpaceId { get; set; }
    
    [Required]
    public required string Side { get; set; }
    [Required]
    public required string RefToImage { get; set; }
    
    [Required]
    [ForeignKey("AdSpaceTypeId")]
    public Guid AdSpaceTypeId { get; set; }
    
    [Required]
    [ForeignKey("CarrierId")]
    public Guid CarrierId { get; set; }
    
    public required AdSpaceType AdSpaceType { get; set; }
    public required Carrier Carrier { get; set; }
    
    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdSpaceInPreset>? AdSpaceInPresets { get; set; }
    public ICollection<AdSpacePrice>? AdSpacePrices { get; set; }
}