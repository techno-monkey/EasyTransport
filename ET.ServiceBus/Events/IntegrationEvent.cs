using System.Text.Json.Serialization;

namespace ET.ServiceBus.Events
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonInclude]
        public Guid Id { get; private init; }

        [JsonInclude]
        public DateTime CreationDate { get; private init; }
    }

    public class SubscriptionInfo
    {
        public Type HandlerType { get; }

        private SubscriptionInfo( Type handlerType)
        {
            HandlerType = handlerType;
        }


        public static SubscriptionInfo Typed(Type handlerType) =>
            new SubscriptionInfo(handlerType);
    }

}
