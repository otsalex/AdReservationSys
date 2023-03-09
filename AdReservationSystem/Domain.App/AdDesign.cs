using System.ComponentModel.DataAnnotations;
using Domain.Contracts;

namespace Domain.App;

public class AdDesign
{
    public Guid AdDesignId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required  string Name { get; set; }
    
    public required string RefToImage { get; set; }
    
    public ICollection<AdSpaceInReservation>? AdSpaceInReservations { get; set; }
    public ICollection<AdDesignInReservation>? AdDesignInReservations { get; set; }
    
}   