using AutoMapper;

namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Domain.App.Reservation, v1.ReservationWithAdSpaces>();
        CreateMap<Domain.App.Reservation, v1.ReservationWOAdSpaces>();
        CreateMap<Domain.App.Reservation, v1.Reservation>();
        CreateMap<Domain.App.AdSpace, v1.AdSpaceMin>();
        CreateMap<Domain.App.Carrier, v1.CarrierMin>();
        CreateMap<Domain.App.Carrier, v1.CarrierWithAdSpaces>();
    }
}