﻿using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>(b =>
        {
            // Primary key
            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("AppUsers");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);
            
            
            
            // b.HasMany(e => e.AppRefreshTokens)
            //     .WithOne()
            //     .HasForeignKey(ut => ut.AppUserId)
            //     .IsRequired();
            //
            
        });
    }

    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    public DbSet<AppUser> AppUsers { get; set; }
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