using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Reservation : DomainEntityId
{
    [Required]
    [MaxLength(255)]
    public required string CampaignName { get; set; }
    
    [Required]
    public required string State { get; set; }
    
    [Required]
    public DateTime CreationTime { get; set; }

    public string City { get; set; } = default!;
    public DateTime? ApprovalTime { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; }
    
    
    [Required]
    [ForeignKey("AppUserId")]
    public Guid AppUserId { get; set; }
    
    public AppUser? User { get; set; }

    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdDesignInReservation>? AdDesignInReservations { get; set; }
}