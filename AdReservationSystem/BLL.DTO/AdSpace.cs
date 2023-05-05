using Domain.App;
using Domain.Base;

namespace BLL.DTO;

public class AdSpace : DomainEntityId
{
    public required string Side { get; set; }
    public required string RefToImage { get; set; }
    public Guid AdSpaceTypeId { get; set; }
    public Guid CarrierId { get; set; }
    public  AdSpaceType? AdSpaceType { get; set; }
    public  Carrier? Carrier { get; set; }
    public ICollection<AdSpaceInReservation> AdSpaceInReservations { get; set; } = default!;
    public ICollection<AdSpaceInPreset> AdSpaceInPresets { get; set; } = default!;
    public ICollection<AdSpacePrice> AdSpacePrices { get; set; } = default!;
}