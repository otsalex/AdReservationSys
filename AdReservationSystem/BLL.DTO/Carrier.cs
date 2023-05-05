using Domain.App;
using Domain.Base;

namespace BLL.DTO;

public class Carrier : DomainEntityId
{
    public required string City { get; set; }
    public required string Number { get; set; }
    public double GPSX { get; set; }
    public double GPSY { get; set; }
    
    public string? BusStopName { get; set; }
    public string? Street { get; set; }
    public string? Direction { get; set; }
    
    public Guid CarrierTypeId { get; set; }
    
    public CarrierType? CarrierType { get; set; } = default!;
    public ICollection<AdSpace> AdSpaces { get; set; } = default!;
}