using DAL.Contacts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ICarrierService: IBaseRepository<BLL.DTO.Carrier>, ICarrierRepositoryCustom<BLL.DTO.Carrier>
{
    
}