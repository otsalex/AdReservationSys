using System.ComponentModel.DataAnnotations;

namespace Domain;

public class PresetType
{
    public Guid PresetTypeId { get; set; }
    
    [Required]
    public required string Type { get; set; }

    public ICollection<Preset>? Presets { get; set; }
}