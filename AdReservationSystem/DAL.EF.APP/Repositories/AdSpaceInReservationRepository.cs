using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdSpaceInReservationRepository : EfBaseRepository<AdSpaceInReservation, ApplicationDbContext>, IAdSpaceInReservationRepository
{
    public AdSpaceInReservationRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}