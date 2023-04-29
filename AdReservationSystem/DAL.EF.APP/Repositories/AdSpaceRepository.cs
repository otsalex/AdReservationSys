using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AdSpaceRepository : EfBaseRepository<AdSpace, ApplicationDbContext>, IAdSpaceRepository
{
    public AdSpaceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    public override async Task<IEnumerable<AdSpace?>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c!.Carrier)
            .OrderBy(e => e!.AdSpaceType)
            .ToListAsync();
    }
    
    public override async Task< AdSpace?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}