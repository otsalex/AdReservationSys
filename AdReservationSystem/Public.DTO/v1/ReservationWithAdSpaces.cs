using Domain.App;

namespace Public.DTO.v1;

public class ReservationWithAdSpaces
{
    public Guid Id { get; set; }
    public string CampaignName { get; set; } = default!;
    public string State { get; set; } = default!;
    public string City { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime ApprovalTime { get; set; }

    public ICollection<AdSpaceMin> AdSpaces { get; set; } = default!;
}