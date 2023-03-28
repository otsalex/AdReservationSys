using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class PresetType : DomainEntityId
{
    [Required]
    [MaxLength(255)]
    public required string Type { get; set; }

    public ICollection<Preset>? Presets { get; set; }
}