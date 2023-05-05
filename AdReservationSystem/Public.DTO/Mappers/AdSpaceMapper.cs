using AutoMapper;
using DAL.Base;
using Domain.App;
using Public.DTO.v1;
using AdSpace = BLL.DTO.AdSpace;

namespace Public.DTO.Mappers;

public class AdSpaceMapper: BaseMapper<BLL.DTO.AdSpace, AdSpaceMin>
{
    public AdSpaceMapper(IMapper mapper) : base(mapper)
    {
    }
    public v1.AdSpaceMin? MapWithAdSpaces(Domain.App.AdSpace entity)
    {
        var res = Mapper.Map<v1.AdSpaceMin>(entity);
        return res;
    }
}