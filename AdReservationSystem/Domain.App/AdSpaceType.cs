using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class AdSpaceType : DomainEntityId
{
    [Required]
    [MaxLength(255)]
    public required string Type { get; set; }
    
    public float? Height { get; set; }
    public float? Width { get; set; }
    public string? Material { get; set; }
    public string? Description { get; set; }
    
    public ICollection<AdSpace>? AdSpaces { get; set; }
}