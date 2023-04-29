using Domain.App;

namespace DAL.DTO;

public class ReservationWithoutAdSpaces
{
    public required string CampaignName { get; set; }
    public required string State { get; set; }


    public string City { get; set; } = default!;
    public DateTime? ApprovalTime { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}