using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contacts.App;

public interface ICarrierRepository : IBaseRepository<Carrier>, ICarrierRepositoryCustom<Carrier>
{
    
}
public interface ICarrierRepositoryCustom<TEntity>
{
}