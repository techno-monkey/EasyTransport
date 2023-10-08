using ET.Models.DtoModels.Bus;

namespace ET.Trans.Bus.Interface
{
    public interface IBusService
    {
        Task<IEnumerable<BusDto>> Get();
        Task<BusDto> Get(Guid id);
        Task<BusDto> Create(ET.Models.DataBase.Transport.Bus.Bus obj);
    }
}
