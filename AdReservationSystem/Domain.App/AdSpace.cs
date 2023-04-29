using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    //[JsonIgnore]
    public  Carrier? Carrier { get; set; }
    
    public ICollection<AdSpaceInReservation> AdSpaceInReservations { get; set; } = default!;
    public ICollection<AdSpaceInPreset> AdSpaceInPresets { get; set; } = default!;
    public ICollection<AdSpacePrice> AdSpacePrices { get; set; } = default!;
}