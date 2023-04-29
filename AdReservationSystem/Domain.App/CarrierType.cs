using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class CarrierType : DomainEntityId
{
    [Required]
    [MaxLength(255)]
    public required string Type { get; set; }
    public ICollection<CarrierType>? CarrierTypes { get; set; }
}