using DAL.Contacts.App;
using DAL.Contracts.Base;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdSpaceTypeRepository : EfBaseRepository<AdSpaceType, ApplicationDbContext>, IAdSpaceTypeRepository
{
    public AdSpaceTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}