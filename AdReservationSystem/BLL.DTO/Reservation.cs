using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.DTO;

public class Reservation : DomainEntityId
{

    public required string CampaignName { get; set; }
    
    public required string State { get; set; }
    
    public DateTime CreationTime { get; set; }

    public string City { get; set; } = default!;
    public DateTime? ApprovalTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Guid AppUserId { get; set; }
    
    public AppUser? User { get; set; }

    public ICollection<AdSpace>? AdSpaces { get; set; }
    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdDesignInReservation>? AdDesignInReservations { get; set; }
}