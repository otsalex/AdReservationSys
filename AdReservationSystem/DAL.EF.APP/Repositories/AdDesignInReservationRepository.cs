using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class AdDesignInReservationRepository: EfBaseRepository<AdDesignInReservation, ApplicationDbContext>, IAdDesignInReservationRepository
{
    public AdDesignInReservationRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}