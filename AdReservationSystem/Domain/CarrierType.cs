using System.ComponentModel.DataAnnotations;

namespace Domain;

public class CarrierType
{
    public Guid CarrierTypeId { get; set; }
    
    [Required]
    public required string Type { get; set; }
}