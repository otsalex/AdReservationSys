using System.Globalization;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeding;

public static class AppDataInit
{
    private static readonly Guid AdminId = Guid.Parse("bc7458ac-cbb0-4ecd-be79-d5abf19f8c77");
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }
    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }
    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string password) userData = (AdminId, "admin@app.com", "Hajus1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user == null)
        {
            user = new AppUser()
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, userData.password).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Cannot seed identity");
            }
            
        }
    }
    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataReservations(context);
        SeedAppDataAdDesigns(context);
        SeedAppDataAdDesignInReservations(context);
        SeedAppDataCarrierTypes(context);
        SeedAppDataAdSpaceTypes(context);
        SeedAppDataCarriers(context);
        SeedAppDataAdSpaces(context);
        SeedAppDataAdSpaceInReservations(context);
        SeedAppDataAdSpacePrices(context);
        SeedAppDataPresetTypes(context);
        SeedAppDataPresets(context);
        SeedAppDataAdSpaceInPresets(context);
        SeedAppDataUsersPresets(context);
        
        
        context.SaveChanges();
    }

    private static Guid _designId;
    private static Guid _reservationId;
    private static Guid _adSpaceId;
    private static Guid _carrierId;
    private static Guid _carrierTypeId;
    private static Guid _adSpaceTypeId;
    private static Guid _presetId;
    private static Guid _presetTypeId;
    private static void SeedAppDataReservations(ApplicationDbContext context)
    {
        if (context.Reservations.Any()) return;

        var reservation = new Reservation()
        {
            CampaignName = "Test Campaign",
            AppUserId = AdminId,
            State = "pending"
        };
        
        context.Reservations.Add(reservation);
        _reservationId = reservation.Id;
    }
    private static void SeedAppDataAdDesigns(ApplicationDbContext context)
    {
        if (context.AdDesigns.Any()) return;
        var design = new AdDesign
        {
            Id = Guid.NewGuid(),
            Name = "MyRoomSpring",
            RefToImage = ""
        };
        
        context.AdDesigns.Add(design);
        _designId = design.Id;
    }
    private static void SeedAppDataAdDesignInReservations(ApplicationDbContext context)
    {
        if (context.AdDesignInReservations.Any()) return;

        context.AdDesignInReservations.Add(new AdDesignInReservation
            {
                Id = Guid.NewGuid(),
                AdDesignId = _designId,
                ReservationId = _reservationId
            }
        );
    }
    private static void SeedAppDataCarrierTypes(ApplicationDbContext context)
    {
        if (context.CarrierTypes.Any()) return;

        var carrierType = new CarrierType
        {
            Id = Guid.NewGuid(),
            Type = "bus shelter"
        };
        
        context.CarrierTypes.Add(carrierType);
        _carrierTypeId = carrierType.Id;
    }
    private static void SeedAppDataAdSpaceTypes(ApplicationDbContext context)
    {
        if (context.AdSpaceTypes.Any()) return;

        var adSpaceType = new AdSpaceType
        {
            Id = Guid.NewGuid(),
            Type = "citylight",
            Height = 1750,
            Width = 1185,
            Material = null,
            Description = null
        };
        context.AdSpaceTypes.Add(adSpaceType);
        _adSpaceTypeId = adSpaceType.Id;
    }
    private static void SeedAppDataCarriers(ApplicationDbContext context)
    {
        if (context.Carriers.Any()) return;

        var carrier = new Carrier
        {
            Id = Guid.NewGuid(),
            City = "Pärnu",
            Number = "B1",
            GPSX = 58.39216376283948,
            GPSY = 24.463635198697105,
            BusStopName = "Vana-Pärnu",
            Street = "Haapsalu mnt",
            Direction = "ingoing",
            CarrierTypeId = _carrierTypeId
        };
        
        context.Carriers.Add(carrier);
        _carrierId = carrier.Id;
    }
    private static void SeedAppDataAdSpaces(ApplicationDbContext context)
    {
        if (context.AdSpaces.Any()) return;

        var adSpace = new AdSpace
        {
            Id = Guid.NewGuid(),
            Side = "inner",
            RefToImage = "",
            AdSpaceTypeId = _adSpaceTypeId,
            CarrierId = _carrierId
        };
        
        context.AdSpaces.Add(adSpace);
        _adSpaceId = adSpace.Id;
    }
    private static void SeedAppDataAdSpaceInReservations(ApplicationDbContext context)
    {
        if (context.AdSpaceInReservations.Any()) return;

        context.AdSpaceInReservations.Add(new AdSpaceInReservation
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now,
                EndTime = null,
                ReservationId = _reservationId,
                AdDesignId = _designId,
                AdSpaceId = _adSpaceId
            }
        );
    }
    
    private static void SeedAppDataAdSpacePrices(ApplicationDbContext context)
    {
        if (context.AdSpacePrices.Any()) return;

        context.AdSpacePrices.Add(new AdSpacePrice
            {
                Id = Guid.NewGuid(),
                Price = 20,
                StartTime = DateTime.ParseExact("2023-03-20 14:26", "yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture),
                EndTime = null,
                AdSpaceId = _adSpaceId
            }
        );
    }
    private static void SeedAppDataPresetTypes(ApplicationDbContext context)
    {
        if (context.PresetTypes.Any()) return;

        var presetType = new PresetType
        {
            Id = Guid.NewGuid(),
            Type = "premade"
        };
        context.PresetTypes.Add(presetType);
        _presetTypeId = presetType.Id;
    }
    private static void SeedAppDataPresets(ApplicationDbContext context)
    {
        if (context.Presets.Any()) return;

        var preset = new Preset
        {
            Id = Guid.NewGuid(),
            Name = "Pärnu koolid",
            PresetTypeId = _presetTypeId
        };
        context.Presets.Add(preset);
        _presetId = preset.Id;
    }
    private static void SeedAppDataAdSpaceInPresets(ApplicationDbContext context)
    {
        if (context.AdSpaceInPresets.Any()) return;

        context.AdSpaceInPresets.Add(new AdSpaceInPreset
            {
                Id = Guid.NewGuid(),
                AdSpaceId = _adSpaceId,
                PresetId = _presetId
            }
        );
    }
    private static void SeedAppDataUsersPresets(ApplicationDbContext context)
    {
        if (context.UsersPresets.Any()) return;

        context.UsersPresets.Add(new UsersPreset
            {
                Id = Guid.NewGuid(),
                PresetId = _presetId,
                AppUserId = AdminId
            }
        );
    }
}

