using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ReservationRepository : EfBaseRepository<Reservation, ApplicationDbContext>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Reservation?>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e!.CampaignName)
            .OrderBy(e => e!.State)
            .ToListAsync();
    }
}