namespace ET.Orders.ServiceBus
{
    public interface IOrderEventService
    {
        Task PublishEventsAsync(Guid transactionId);
    }


}
