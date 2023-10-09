using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace ET.ServiceBus
{
    public class ServiceBusConnection : IServiceBusConnection
    {
        private readonly string _serviceBusConnectionString;
        private ServiceBusClient _topicClient;
        private ServiceBusAdministrationClient _subscriptionClient;
        bool _disposed;
        public ServiceBusConnection(string serviceBusConnectionString)
        {
            _serviceBusConnectionString = serviceBusConnectionString;
            _subscriptionClient = new ServiceBusAdministrationClient(_serviceBusConnectionString);
            _topicClient = new ServiceBusClient(_serviceBusConnectionString, new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpTcp,
                RetryOptions = new ServiceBusRetryOptions
                {
                    TryTimeout = TimeSpan.FromSeconds(60),
                    MaxRetries = 3,
                    Delay = TimeSpan.FromSeconds(.8)
                }
            });

        }

        public ServiceBusClient TopicClient
        {
            get
            {
                if (_topicClient.IsClosed)
                {
                    _topicClient = new ServiceBusClient(_serviceBusConnectionString);
                }
                return _topicClient;
            }
        }

        public ServiceBusAdministrationClient AdministrationClient => _subscriptionClient;

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            _disposed = true;
            await _topicClient.DisposeAsync();
        }
    }
}
