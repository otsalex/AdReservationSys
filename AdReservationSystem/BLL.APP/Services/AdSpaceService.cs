using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contacts.App;

namespace BLL.App.Services;

public class AdSpaceService :
    BaseEntityService<BLL.DTO.AdSpace, Domain.App.AdSpace, IAdSpaceRepository>, IAdSpaceService
{
    protected IAppUOW Uow;
    
    public AdSpaceService(IAppUOW uow, IMapper<BLL.DTO.AdSpace, Domain.App.AdSpace> mapper) 
        : base(uow.AdSpaceRepository, mapper)
    {
        Uow = uow;
    }
}