using AutoMapper;
using BLL.APP.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contacts.App;
using CarrierMapper = BLL.APP.Mappers.CarrierMapper;
using ReservationMapper = BLL.APP.Mappers.ReservationMapper;


namespace BLL.APP;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private IReservationService? _reservationService;
    public IReservationService ReservationService =>
        _reservationService ??= new ReservationService(Uow, new ReservationMapper(_mapper));
    
    private ICarrierService? _carrierService;
    public ICarrierService CarrierService =>
        _carrierService ??= new CarrierService(Uow, new CarrierMapper(_mapper));
    
    private IAdSpaceService? _adSpaceService;
    public IAdSpaceService AdSpaceService =>
        _adSpaceService ??= new AdSpaceService(Uow, new AdSpaceMapper(_mapper));

 
}