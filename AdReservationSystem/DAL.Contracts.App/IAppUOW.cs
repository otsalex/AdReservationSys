using DAL.Contracts.Base;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Contacts.App;

public interface IAppUOW : IBaseUOW

{
    public IdentityDbContext<AppUser, AppRole, Guid> _ctx { get; set; }
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
    IAppUserRepository AppUserRepository { get; }
}