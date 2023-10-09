using ET.ServiceBus.Events;

namespace ET.Orders.ServiceBus
{
    public interface IOrderEventService
    {
        Task PublishEventsAsync(IntegrationEvent @event);
    }

}
