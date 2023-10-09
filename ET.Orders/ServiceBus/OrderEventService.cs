using ET.ServiceBus;

namespace ET.Orders.ServiceBus
{
    public class OrderEventService : IOrderEventService
    {
        private readonly ILogger<OrderEventService> _logger;
        private readonly IEventBusServiceBus _eventBus;
        public Task PublishEventsAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
