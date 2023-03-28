using DAL.Contacts.App;
using DAL.Contracts.Base;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repositories;

public class CarrierRepository: EfBaseRepository<Carrier, ApplicationDbContext>, ICarrierRepository
{
    public CarrierRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}