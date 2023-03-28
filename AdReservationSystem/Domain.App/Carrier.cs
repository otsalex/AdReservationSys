using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class Carrier : DomainEntityId
{
    [Required]
    public required string City { get; set; }
    [Required]
    public required string Number { get; set; }
    [Required]
    public double GPSX { get; set; }
    [Required]
    public double GPSY { get; set; }
    
    public string? BusStopName { get; set; }
    public string? Street { get; set; }
    public string? Direction { get; set; }
    
    [Required]
    [ForeignKey("CarrierTypeId")]
    public Guid CarrierTypeId { get; set; }
    
    public CarrierType? CarrierType { get; set; }
    
    public ICollection<CarrierType>? CarrierTypes { get; set; }
}