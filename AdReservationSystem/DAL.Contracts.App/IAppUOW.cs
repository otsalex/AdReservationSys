using DAL.Contacts.App;
using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    // list your repositories here
    IAdDesignInReservationRepository AdDesignInReservationRepository { get; }
    IAdDesignRepository AdDesignRepository { get; }
    IAdSpaceInPresetRepository AdSpaceInPresetRepository { get; }
    IAdSpaceInReservationRepository AdSpaceInReservationRepository { get; }
    IAdSpacePriceRepository AdSpacePriceRepository { get; }
    IAdSpaceRepository AdSpaceRepository { get; }
    IAdSpaceTypeRepository AdSpaceTypeRepository { get; }
    ICarrierRepository CarrierRepository { get; }
    ICarrierTypeRepository CarrierTypeRepository { get; }
    IPresetRepository PresetRepository { get; }
    IPresetTypeRepository PresetTypeRepository { get; }
    IReservationRepository ReservationRepository { get; }
    IUsersPresetRepository UsersPresetRepository { get; }
}