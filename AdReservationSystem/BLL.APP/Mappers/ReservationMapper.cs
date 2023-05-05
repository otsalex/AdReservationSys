using AutoMapper;
using DAL.Base;

namespace BLL.APP.Mappers;

public class ReservationMapper : BaseMapper<BLL.DTO.Reservation, Domain.App.Reservation>
{
    public ReservationMapper(IMapper mapper) : base(mapper)
    {
    }
}