using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdSpacePriceRepository : EfBaseRepository<AdSpacePrice, ApplicationDbContext>, IAdSpacePriceRepository
{
    public AdSpacePriceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}