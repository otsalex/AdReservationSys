using AutoMapper;
using DAL.Base;

namespace BLL.APP.Mappers;

public class AdSpaceMapper: BaseMapper<BLL.DTO.AdSpace, Domain.App.AdSpace>
{
    public AdSpaceMapper(IMapper mapper) : base(mapper)
    {
    }
}