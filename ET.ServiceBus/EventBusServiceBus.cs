using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using ET.ServiceBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ET.ServiceBus
{
    public interface IEventBusServiceBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }

    public class EventBusServiceBus : IEventBusServiceBus, IAsyncDisposable
    {
        private readonly IServiceBusConnection _serviceBusConnection;
        private readonly ILogger<EventBusServiceBus> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly string _topicName = "order_event";
        private readonly string _subscriptionName;
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusProcessor _processor;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";
        private readonly IServiceProvider _serviceProvider;

        public EventBusServiceBus(IServiceBusConnection serviceBusConnection,
       ILogger<EventBusServiceBus> logger, IEventBusSubscriptionsManager subsManager, string subscriptionClientName, IServiceProvider serviceProvider)
        {
            _serviceBusConnection = serviceBusConnection;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new EventBusSubscriptionsManager();
            _subscriptionName = subscriptionClientName;
            _sender = _serviceBusConnection.TopicClient.CreateSender(_topicName);
            ServiceBusProcessorOptions options = new ServiceBusProcessorOptions { MaxConcurrentCalls = 10, AutoCompleteMessages = false };
            _processor = _serviceBusConnection.TopicClient.CreateProcessor(_topicName, _subscriptionName, options);
            _serviceProvider = serviceProvider;
            RemoveDefaultRule();
            RegisterSubscriptionClientMessageHandlerAsync().GetAwaiter().GetResult();
        }
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public void Publish(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }


        private void RemoveDefaultRule()
        {
            try
            {
                _serviceBusConnection
                    .AdministrationClient
                    .DeleteRuleAsync(_topicName, _subscriptionName, RuleProperties.DefaultRuleName)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
            {
                _logger.LogWarning("The messaging entity {DefaultRuleName} Could not be found.", RuleProperties.DefaultRuleName);
            }
        }

        private async Task RegisterSubscriptionClientMessageHandlerAsync()
        {
            _processor.ProcessMessageAsync +=
                async (args) =>
                {
                    var eventName = $"{args.Message.Subject}{INTEGRATION_EVENT_SUFFIX}";
                    string messageData = args.Message.Body.ToString();

                    // Complete the message so that it is not received again.
                    if (await ProcessEvent(eventName, messageData))
                    {
                        await args.CompleteMessageAsync(args.Message);
                    }
                };

            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            var ex = args.Exception;
            var context = args.ErrorSource;

            _logger.LogError(ex, "Error handling message - Context: {@ExceptionContext}", context);

            return Task.CompletedTask;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;
            await using var scope = _serviceProvider.CreateAsyncScope();
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new[] { integrationEvent });
                }
            }
            processed = true;
            return processed;
        }
    }
}
