using AutoMapper;
using DAL.Base;
using Domain.App;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class AdSpaceMapper: BaseMapper<AdSpace, AdSpaceMin>
{
    public AdSpaceMapper(IMapper mapper) : base(mapper)
    {
    }
}