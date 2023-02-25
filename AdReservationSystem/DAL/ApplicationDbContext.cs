using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

   
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    // }

    public DbSet<AdDesign> AdDesigns { get; set; }
    public DbSet<AdDesignInReservation> AdDesignInReservations { get; set; }
    public DbSet<AdSpace> AdSpaces { get; set; }
    public DbSet<AdSpaceInReservation> AdSpaceInReservations { get; set; }
    public DbSet<AdSpaceInPreset> AdSpaceInPresets { get; set; }
    public DbSet<AdSpacePrice> AdSpacePrices { get; set; }
    public DbSet<AdSpaceType> AdSpaceTypes { get; set; }
    public DbSet<Carrier> Carriers { get; set; }
    public DbSet<CarrierType> CarrierTypes { get; set; }
    public DbSet<Preset> Presets { get; set; }
    public DbSet<PresetType> PresetTypes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<UsersPreset> UsersPresets { get; set; }
}