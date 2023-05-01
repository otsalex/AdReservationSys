using System.ComponentModel.DataAnnotations;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public virtual ICollection<Reservation>? Reservations { get; set; }

    public virtual ICollection<UsersPreset>? UsersPreset { get; set; }
    public virtual ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}