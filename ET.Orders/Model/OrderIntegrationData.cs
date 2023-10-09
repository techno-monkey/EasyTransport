using ET.Models.DtoModels.Order;
using ET.ServiceBus.Events;

namespace ET.Orders.Model
{
    public record OrderIntegrationData(OrderDto OrderDtoObj) : IntegrationEvent;
}
