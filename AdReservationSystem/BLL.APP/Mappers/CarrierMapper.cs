using AutoMapper;
using DAL.Base;

namespace BLL.APP.Mappers;

public class CarrierMapper: BaseMapper<BLL.DTO.Carrier, Domain.App.Carrier>
{
    public CarrierMapper(IMapper mapper) : base(mapper)
    {
    }
}