using DAL.Contacts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IReservationService : IBaseRepository<BLL.DTO.Reservation>, IReservationRepositoryCustom<BLL.DTO.Reservation>
{
    // add your custom service methods here
}