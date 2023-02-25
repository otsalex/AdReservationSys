using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class AdSpaceInPreset
{
    public Guid AdSpaceInPresetId { get; set; }
    
    [Required]
    [ForeignKey("AdSpaceId")]
    public Guid AdSpaceId { get; set; }
    
    [Required]
    [ForeignKey("PresetId")]
    public Guid PresetId { get; set; }
    
    public required AdSpace AdSpace { get; set; }
    public required Preset Preset { get; set; }
}