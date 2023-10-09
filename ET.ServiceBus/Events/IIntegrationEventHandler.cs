namespace ET.ServiceBus.Events
{
    public interface IIntegrationEventHandler
    {
    }
    public interface IIntegrationEventHandler<in  TEvent>: IIntegrationEventHandler where TEvent : IntegrationEvent
    {
    }
}
