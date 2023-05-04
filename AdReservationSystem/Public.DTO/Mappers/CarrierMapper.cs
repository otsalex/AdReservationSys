using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class CarrierMapper: BaseMapper<Carrier, Public.DTO.v1.CarrierMin>
{
    public CarrierMapper(IMapper mapper) : base(mapper)
    {
    }
    public v1.CarrierWithAdSpaces? MapWithAdSpaces(Carrier entity)
    {
        var res = Mapper.Map<v1.CarrierWithAdSpaces>(entity);
        return res;
    }
}