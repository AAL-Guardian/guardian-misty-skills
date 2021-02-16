using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Data;
using CloudConnector.Events;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;

namespace CloudConnector.Services
{
    public sealed class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MistyConfiguration _mistyConfiguration;

        public event MqttMessageReceivedHandler MqttMessageReceived;
        
        public MqttService(MistyConfiguration mistyConfiguration)
        {
            _mistyConfiguration = mistyConfiguration;
            _mqttClient = new MqttFactory().CreateMqttClient();
        }

        public IAsyncAction Start()
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

            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                dynamic payloadObj = JsonConvert.DeserializeObject(payload);
                MqttMessageReceivedData data = new MqttMessageReceivedData();
                data.command = payloadObj.command;
                data.data = payloadObj.data;
                OnMqttMessageReceived(data);
            });

            return _mqttClient.ConnectAsync(options, CancellationToken.None).AsAsyncAction();
        }

        public async void OnMistyMessage(object sender, MistyMessageReceivedData data)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(_mistyConfiguration.RobotTopic)
                .WithPayload(JsonConvert.SerializeObject(data))
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
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