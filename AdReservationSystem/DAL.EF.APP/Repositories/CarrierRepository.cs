using DAL.Contacts.App;
using DAL.Contracts.Base;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CarrierRepository: EfBaseRepository<Carrier, ApplicationDbContext>, ICarrierRepository
{
    public CarrierRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    public override async Task<IEnumerable<Carrier?>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c!.AdSpaces)
            .OrderBy(e => e!.City)
            .ToListAsync();
    }
    public override async Task< Carrier?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}