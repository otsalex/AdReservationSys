using System.ComponentModel.DataAnnotations;

namespace Domain.App;

public class CarrierType
{
    public Guid CarrierTypeId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Type { get; set; }
}