using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdDesignRepository : EfBaseRepository<AdDesign, ApplicationDbContext>, IAdDesignRepository
{
    public AdDesignRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}