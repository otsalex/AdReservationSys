using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AdSpaceInReservationRepository : EfBaseRepository<AdSpaceInReservation, ApplicationDbContext>, IAdSpaceInReservationRepository
{
    public AdSpaceInReservationRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    public async Task<bool> RemoveRelationsAsync(Guid reservationId)
    {
        var rels = await RepositoryDbSet
            .Where(a=> a.ReservationId == reservationId)
            .ToListAsync();
        foreach (var rel in rels)
        {
            RepositoryDbSet.Remove(rel);
        }

        return true;
    }
}