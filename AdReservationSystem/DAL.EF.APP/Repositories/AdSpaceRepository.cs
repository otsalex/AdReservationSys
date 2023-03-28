using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdSpaceRepository: EfBaseRepository<AdSpace, ApplicationDbContext>, IAdSpaceRepository
{
    public AdSpaceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}