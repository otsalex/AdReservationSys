using BLL.Contracts.Base;
using DAL.Contacts.App;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    IAppUOW Uow { get; set; }
    IReservationService ReservationService { get; }
    ICarrierService CarrierService { get; }
    IAdSpaceService AdSpaceService { get; }
}