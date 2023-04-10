using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contacts.App;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    public Task<IEnumerable<Reservation>> AllAsync(Guid userId);
    public Task<Reservation?> FindAsync(Guid id, Guid userId);
}