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

    public override async Task<IEnumerable< Reservation>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.User)
            .OrderBy(e => e.CampaignName)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable< Reservation>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            //.Include(e => e.AppUser)
            .OrderBy(e => e.CampaignName)
            .Where(t => t.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task< Reservation?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(t => t.User)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task< Reservation?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(t => t.User)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }
}