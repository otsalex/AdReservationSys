using Domain.Contracts;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public override Guid Id { get; set; }
    
    public virtual ICollection<Reservation>? Reservations { get; set; }

    public virtual ICollection<UsersPreset>? UsersPreset { get; set; }
    public virtual ICollection<IdentityUserClaim<Guid>>? Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<Guid>>? Logins { get; set; }
    public virtual ICollection<IdentityUserToken<Guid>>? Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<Guid>>? UserRoles { get; set; }
}