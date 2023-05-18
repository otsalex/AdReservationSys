using AutoMapper;

namespace BLL.APP;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.Reservation, Domain.App.Reservation>().ReverseMap();
        CreateMap<BLL.DTO.AdSpace, Domain.App.AdSpace>().ReverseMap();
        CreateMap<BLL.DTO.Carrier, Domain.App.Carrier>().ReverseMap();
    }
}