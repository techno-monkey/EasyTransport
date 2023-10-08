using ET.Models.DataBase.Order;
using ET.Models.DtoModels.Order;

namespace ET.Orders.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> Get();
        Task<OrderDto> Get(Guid id);
        Task<OrderDto> Create(Order obj);
    }
}
