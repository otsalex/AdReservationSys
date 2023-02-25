using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Preset
{
    public Guid PresetId { get; set; }
    public string Name { get; set; }
    
    [Required]
    [ForeignKey("PresetTypeId")]
    public Guid PresetTypeId { get; set; }
    
    public required PresetType PresetType { get; set; }
    
    public ICollection<AdSpaceInPreset>? AdSpaceInPresets { get; set; }
    public ICollection<UsersPreset>? UsersPresets { get; set; }
}