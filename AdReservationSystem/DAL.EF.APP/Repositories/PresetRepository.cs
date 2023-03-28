using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class PresetRepository : EfBaseRepository<Preset, ApplicationDbContext>, IPresetRepository
{
    public PresetRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}