using ET.Models.DataBase.Transport.Bus;
using ET.Models.DtoModels.Bus;

namespace ET.Trans.Bus.Interface
{
    public interface ITransporterService
    {
        Task<IEnumerable<TransporterDto>> Get();
        Task<TransporterDto> Get(Guid id);
        Task<TransporterDto> Create(Transporter obj);
    }
}
