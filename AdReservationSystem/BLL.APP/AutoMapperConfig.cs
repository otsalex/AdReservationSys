using AutoMapper;

namespace BLL.APP;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.Reservation, Domain.App.Reservation>().ReverseMap();
    }
}