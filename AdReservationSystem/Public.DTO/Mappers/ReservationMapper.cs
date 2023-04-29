using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class ReservationMapper: BaseMapper<Reservation, Public.DTO.v1.Reservation>
{
    public ReservationMapper(IMapper mapper) : base(mapper)
    {
    }

    public v1.ReservationWithAdSpaces? MapWithAdSpaces(Reservation entity)
    {
        var res = Mapper.Map<v1.ReservationWithAdSpaces>(entity);
        return res;
    }
    public v1.ReservationWOAdSpaces? MapWOAdSpaces(Reservation entity)
    {
        var res = Mapper.Map<v1.ReservationWOAdSpaces>(entity);
        return res;
    }

}