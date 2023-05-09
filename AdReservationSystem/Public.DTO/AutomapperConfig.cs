using AutoMapper;

namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Reservation, v1.ReservationWithAdSpaces>();
        CreateMap<BLL.DTO.Reservation, v1.ReservationWOAdSpaces>();
        CreateMap<BLL.DTO.Reservation, v1.Reservation>();
        CreateMap<BLL.DTO.AdSpace, v1.AdSpaceMin>();
        CreateMap<BLL.DTO.Carrier, v1.CarrierMin>();
        CreateMap<BLL.DTO.Carrier, v1.CarrierWithAdSpaces>();
    }
}