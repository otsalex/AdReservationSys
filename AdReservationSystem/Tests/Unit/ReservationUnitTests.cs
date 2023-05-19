using AutoMapper;
using BLL.APP;
using BLL.APP.Mappers;
using BLL.App.Services;
using BLL.Contracts.App;
using DAL;
using Microsoft.EntityFrameworkCore;

using Reservation = Domain.App.Reservation;

// using OwnersController = WebApp.Api.OwnersController;

namespace Tests.Unit;

public class ReservationUnitTests
{
   
    private readonly IReservationService? _service;
    private readonly IMapper _mapper;
    private readonly AppUOW _uow;

    public ReservationUnitTests()
    {
        

        // set up mock database - inMemory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        var ctx = new ApplicationDbContext(optionsBuilder.Options);

        // reset db
        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();

        _uow = new AppUOW(ctx);
        // mock mapper
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperConfig()); });
            _mapper = mappingConfig.CreateMapper();

            // SUT
            _service = new ReservationService(_uow, new ReservationMapper(_mapper));
        }
    }

    [Fact(DisplayName = "GET - get all Reservations")]
    public async Task TestGetReservations()
    {
        // Arrange
        // Seed the data
        await SeedDataAsync();
        
        // Act
        var result = await _service!.AllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("test1", result.First()!.CampaignName);
    }

    [Fact(DisplayName = "GET - get reservation by ID")]
    public async Task GetReservationById()
    {
        var reservationId = await SeedDataAsync();
        var result = await _service!.FindAsync(reservationId);
        
        Assert.NotNull(result);
    }
    
    
    private async Task<Guid> SeedDataAsync()
    {
        var reservation = _service!.Add(
            _mapper.Map<Reservation, BLL.DTO.Reservation>(new Reservation()
        {
            CampaignName = "test1",
            State = "pending",
            City = "Pärnu"
        }));
        
        
        _service.Add(
            _mapper.Map<Reservation, BLL.DTO.Reservation>(new Reservation()
        {
            CampaignName = "test2",
            State = "pending",
            City = "Tallinn"
        }));
        
        await _uow.SaveChangesAsync();
        return reservation.Id;
    }
}
