using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class PresetTypeRepository : EfBaseRepository<PresetType, ApplicationDbContext>, IPresetTypeRepository
{
    public PresetTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}