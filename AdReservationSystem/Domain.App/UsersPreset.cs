using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class UsersPreset : DomainEntityId
{
    [Required]
    [ForeignKey("PresetId")]
    public Guid PresetId { get; set; }
    public Preset? Preset { get; set; }
    
    [Required]
    [ForeignKey("AppUserId")]
    public Guid AppUserId { get; set; }
    public virtual AppUser? User { get; set; }
}
