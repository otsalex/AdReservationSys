using System.ComponentModel.DataAnnotations;

namespace Domain.App;

public class AdSpaceType
{
    public Guid AdSpaceTypeId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Type { get; set; }
    
    public float? Height { get; set; }
    public float? Width { get; set; }
    public string? Material { get; set; }
    public string? Description { get; set; }
    
    public ICollection<AdSpace>? AdSpaces { get; set; }
}