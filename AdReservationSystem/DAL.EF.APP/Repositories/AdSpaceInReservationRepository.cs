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
    // public async Task<IEnumerable<AdSpaceInReservation?>> AllByReservationAsync(Guid reservationId)
    // {
    //     return await RepositoryDbSet
    //         .Where(a=> a.ReservationId == reservationId)
    //         .ToListAsync();
    // }
}