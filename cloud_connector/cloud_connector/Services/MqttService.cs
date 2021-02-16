using System;
using System.Threading;
using System.Threading.Tasks;
using CloudConnector.Data;
using CloudConnector.Events;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace CloudConnector.Services
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MistyConfiguration _mistyConfiguration;

        public event MqttMessageReceivedHandler MqttMessageReceived;
        
        public MqttService(MistyConfiguration mistyConfiguration)
        {
            _mistyConfiguration = mistyConfiguration;
            _mqttClient = new MqttFactory().CreateMqttClient();
        }

        public async Task SendMessageAsync(string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(_mistyConfiguration.RobotTopic)
                .WithPayload(payload)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
        }

        public async Task Start()
        {
            // Create TCP based options using the builder.
            var options = new MqttClientOptionsBuilder()
                .WithClientId(_mistyConfiguration.ClientId)
                .WithTcpServer(_mistyConfiguration.Endpoint)
                .Build();

            _mqttClient.UseConnectedHandler(async e =>
            {
                // Subscribe to a topic
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(_mistyConfiguration.RobotTopic).Build());
            });

            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                MqttMessageReceivedData data = new MqttMessageReceivedData();
                OnMqttMessageReceived(data);
            });

            await _mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        public void OnMistyMessage(object sender, MistyMessageReceivedData data)
        {
            
        }
        
        private void OnMqttMessageReceived(MqttMessageReceivedData data)
        {
            MqttMessageReceived?.Invoke(this, data);
        }

        public void Dispose()
        {
            _mqttClient?.Dispose();
        }
    }
}