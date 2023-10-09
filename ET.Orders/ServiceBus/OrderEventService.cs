using ET.ServiceBus;
using ET.ServiceBus.Events;

namespace ET.Orders.ServiceBus
{
    public class OrderEventService : IOrderEventService
    {
        private readonly ILogger<OrderEventService> _logger;
        private readonly IEventBusServiceBus _eventBus;

        public OrderEventService(ILogger<OrderEventService> logger, IEventBusServiceBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task PublishEventsAsync(IntegrationEvent @event)
        {
            try
            {
                _eventBus.Publish(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", @event.Id);
            }
        }
    }
}
