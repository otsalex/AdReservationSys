using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdSpaceInPresetRepository : EfBaseRepository<AdSpaceInPreset, ApplicationDbContext>, IAdSpaceInPresetRepository
{
    public AdSpaceInPresetRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}