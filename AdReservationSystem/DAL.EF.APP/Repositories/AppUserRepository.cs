using DAL.Contacts.App;
using DAL.Contracts.Base;
using DAL.EF.Base;
using Domain.App;
using Domain.App.Identity;

namespace DAL.Repositories;

public class AppUserRepository : EfBaseRepository<AppUser, ApplicationDbContext>, IAppUserRepository
{
    public AppUserRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}