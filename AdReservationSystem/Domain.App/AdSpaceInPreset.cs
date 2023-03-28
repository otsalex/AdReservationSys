using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class AdSpaceInPreset : DomainEntityId
{
    [Required]
    [ForeignKey("AdSpaceId")]
    public Guid AdSpaceId { get; set; }
    
    [Required]
    [ForeignKey("PresetId")]
    public Guid PresetId { get; set; }
    
    public  AdSpace? AdSpace { get; set; }
    public  Preset? Preset { get; set; }
}