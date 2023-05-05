using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contacts.App;

public interface IReservationRepository : IBaseRepository<Reservation>, IReservationRepositoryCustom<Reservation>
{
    // add here custom methods for repo only
}
public interface IReservationRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}