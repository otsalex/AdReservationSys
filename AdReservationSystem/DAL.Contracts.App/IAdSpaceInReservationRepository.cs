using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contacts.App;

public interface IAdSpaceInReservationRepository : IBaseRepository<AdSpaceInReservation>
{
    public Task<bool> RemoveRelationsAsync(Guid reservationId);

}