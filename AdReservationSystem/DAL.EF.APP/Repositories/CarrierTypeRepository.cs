using DAL.Contacts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class CarrierTypeRepository : EfBaseRepository<CarrierType, ApplicationDbContext>, ICarrierTypeRepository
{
    public CarrierTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}