using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    IReservationService ReservationService { get; }
    ICarrierService CarrierService { get; }
    IAdSpaceService AdSpaceService { get; }
}