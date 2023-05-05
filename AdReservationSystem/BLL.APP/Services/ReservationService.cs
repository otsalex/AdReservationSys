using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contacts.App;
using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;
using Public.DTO.v1;
using AdSpace = BLL.DTO.AdSpace;
using Reservation = Domain.App.Reservation;

namespace BLL.App.Services;

public class ReservationService :
    BaseEntityService<BLL.DTO.Reservation, Domain.App.Reservation, IReservationRepository>, IReservationService
{   
    protected IAppUOW Uow;

    public ReservationService(IAppUOW uow, IMapper<BLL.DTO.Reservation, Domain.App.Reservation> mapper)
        : base(uow.ReservationRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<BLL.DTO.Reservation?>> AllAsync()
    {
        return (await Uow.ReservationRepository.AllAsync()).Select(e => Mapper.Map(e));
    }
    public async Task<BLL.DTO.Reservation?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.ReservationRepository.FindAsync(id));
    }

    public async Task<BLL.DTO.Reservation?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.ReservationRepository.RemoveAsync(id));
    }

    public async Task<IEnumerable<BLL.DTO.Reservation>> AllAsync(Guid userId)
    {
        return (await Uow.ReservationRepository.AllAsync(userId)).Select(e => Mapper.Map(e));
    }

    public async Task<BLL.DTO.Reservation?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ReservationRepository.FindAsync(id, userId));
    }

    public BLL.DTO.Reservation Add(Reservation entity)
    {
        return Mapper.Map( Uow.ReservationRepository.Add(entity))!;
    }

    public BLL.DTO.Reservation Update(Reservation entity)
    {
        return Mapper.Map( Uow.ReservationRepository.Update(entity))!;
    }

    public BLL.DTO.Reservation Remove(Reservation entity)
    {
        return Mapper.Map( Uow.ReservationRepository.Remove(entity))!;
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return  Uow.ReservationRepository.IsOwnedByUserAsync(id, userId);
    }
}