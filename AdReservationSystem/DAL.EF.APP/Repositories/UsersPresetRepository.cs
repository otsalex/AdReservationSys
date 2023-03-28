using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class UsersPresetRepository : EfBaseRepository<UsersPreset, ApplicationDbContext>, IUsersPresetRepository
{
    public UsersPresetRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}