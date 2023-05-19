using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App.Identity;

public class AppRefreshToken: BaseRefreshToken
{
    [Required]
    [ForeignKey("AppUserId")]
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}