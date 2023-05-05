using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contacts.App;

namespace BLL.App.Services;

public class CarrierService :
    BaseEntityService<BLL.DTO.Carrier, Domain.App.Carrier, ICarrierRepository>, ICarrierService
{
    protected IAppUOW Uow;
    
    public CarrierService(IAppUOW uow, IMapper<BLL.DTO.Carrier, Domain.App.Carrier> mapper) 
        : base(uow.CarrierRepository, mapper)
    {
        Uow = uow;
    }
    public async Task<IEnumerable<BLL.DTO.Carrier?>> AllAsync()
    {
        return (await Uow.CarrierRepository.AllAsync()).Select(e => Mapper.Map(e));
    }
}