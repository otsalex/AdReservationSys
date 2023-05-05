using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contacts.App;

public interface IAdSpaceRepository : IBaseRepository<AdSpace>, IAdSpaceRepositoryCustom<Domain.App.AdSpace>
{
    
}
public interface IAdSpaceRepositoryCustom<TEntity>
{
}