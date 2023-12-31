﻿using System.Xml.Serialization;
using DAL.Contacts.App;
using DAL.EF.Base;
using DAL.Repositories;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public IdentityDbContext<AppUser, AppRole, Guid> _ctx { get; set; }
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
        _ctx = dataContext;
    }

    private IAdDesignInReservationRepository? _adDesignInReservationRepository;
    private IAdDesignRepository? _adDesignRepository;
    private IAdSpaceInPresetRepository? _adSpaceInPresetRepository;
    private IAdSpaceInReservationRepository? _adSpaceInReservationRepository;
    private IAdSpacePriceRepository? _adSpacePriceRepository;
    private IAdSpaceRepository? _adSpaceRepository;
    private IAdSpaceTypeRepository? _adSpaceTypeRepository;
    private ICarrierRepository? _carrierRepository;
    private ICarrierTypeRepository? _carrierTypeRepository;
    private IPresetRepository? _presetRepository;
    private IPresetTypeRepository? _presetTypeRepository;
    private IReservationRepository? _reservationRepository;
    private IUsersPresetRepository? _usersPresetRepository;
    private IAppUserRepository? _appUserRepository;

    public IAdDesignInReservationRepository AdDesignInReservationRepository =>
        _adDesignInReservationRepository ??= new AdDesignInReservationRepository(UowDbContext);

    public IAdDesignRepository AdDesignRepository =>
        _adDesignRepository ??= new AdDesignRepository(UowDbContext);

    public IAdSpaceInPresetRepository AdSpaceInPresetRepository =>
        _adSpaceInPresetRepository ??= new AdSpaceInPresetRepository(UowDbContext);

    public IAdSpaceInReservationRepository AdSpaceInReservationRepository =>
        _adSpaceInReservationRepository ??= new AdSpaceInReservationRepository(UowDbContext);

    public IAdSpacePriceRepository AdSpacePriceRepository =>
        _adSpacePriceRepository ??= new AdSpacePriceRepository(UowDbContext);

    public IAdSpaceRepository AdSpaceRepository =>
        _adSpaceRepository ??= new AdSpaceRepository(UowDbContext);

    public IAdSpaceTypeRepository AdSpaceTypeRepository =>
        _adSpaceTypeRepository ??= new AdSpaceTypeRepository(UowDbContext);

    public ICarrierRepository CarrierRepository =>
        _carrierRepository ??= new CarrierRepository(UowDbContext);

    public ICarrierTypeRepository CarrierTypeRepository =>
        _carrierTypeRepository ??= new CarrierTypeRepository(UowDbContext);

    public IPresetRepository PresetRepository =>
        _presetRepository ??= new PresetRepository(UowDbContext);

    public IPresetTypeRepository PresetTypeRepository =>
        _presetTypeRepository ??= new PresetTypeRepository(UowDbContext);

    public IReservationRepository ReservationRepository =>
        _reservationRepository ??= new ReservationRepository(UowDbContext);

    public IUsersPresetRepository UsersPresetRepository =>
        _usersPresetRepository ??= new UsersPresetRepository(UowDbContext);
    public IAppUserRepository AppUserRepository =>
        _appUserRepository ??= new AppUserRepository(UowDbContext);
}