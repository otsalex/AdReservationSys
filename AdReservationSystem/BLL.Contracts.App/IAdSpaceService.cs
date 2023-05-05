using DAL.Contacts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAdSpaceService: IBaseRepository<BLL.DTO.AdSpace>, IAdSpaceRepositoryCustom<BLL.DTO.AdSpace>
{
    
}