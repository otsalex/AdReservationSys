using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class Preset : DomainEntityId
{

    [Required]
    [MaxLength(255)]
    public required string  Name { get; set; }
    
    [Required]
    [ForeignKey("PresetTypeId")]
    public Guid PresetTypeId { get; set; }
    
    public  PresetType? PresetType { get; set; }
    
    public ICollection<AdSpaceInPreset>? AdSpaceInPresets { get; set; }
    public ICollection<UsersPreset>? UsersPresets { get; set; }
}