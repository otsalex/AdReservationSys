using DAL.Contacts.App;
using DAL.DTO;
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
            //.Include(e => e.User)
            .OrderBy(e => e!.CampaignName)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<Reservation>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            //.Include(e => e.AppUser)
            .OrderBy(e => e!.CampaignName)
            .Where(t => t!.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task<Reservation?> FindAsync(Guid id)
    {
        var reservation = await RepositoryDbSet
            .Include(r=> r.AdSpaceInReservations)!
            .ThenInclude(a => a.AdSpace)
            .FirstOrDefaultAsync(m => m!.Id == id);
        return reservation;
    }

    public virtual async Task<Reservation?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(t => t.AdSpaceInReservations)!
            .ThenInclude(r => r.AdSpace)
            .ThenInclude(s => s!.Carrier)
            .FirstOrDefaultAsync(m => m!.Id == id && m.AppUserId == userId);
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.AppUserId == userId);
    }
    
}