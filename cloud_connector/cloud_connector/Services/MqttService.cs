using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Data;
using CloudConnector.Events;
using MistyRobotics.SDK.Messengers;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudConnector.Services
{
    public sealed class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MistyConfiguration _mistyConfiguration;
        private readonly IRobotMessenger _misty;

        public event MqttMessageReceivedHandler MqttMessageReceived;
        
        public MqttService(MistyConfiguration mistyConfiguration, IRobotMessenger misty)
        {
            _mistyConfiguration = mistyConfiguration;
            _misty = misty;
            _mqttClient = new MqttFactory().CreateMqttClient();
        }

        public IAsyncAction Start()
        {
            // Create TCP based options using the builder.
            var options = new MqttClientOptionsBuilder()
                .WithClientId(_mistyConfiguration.ClientId)
                .WithWebSocketServer(_mistyConfiguration.Endpoint + "/mqtt?x-amz-customauthorizer-name=GuardianAuthorizer")
                .WithCredentials(_mistyConfiguration.Token, (string)null)
                .Build();
            
            _mqttClient.UseConnectedHandler(async e =>
            {
                await _misty.SendDebugMessageAsync($"Connected to mqtt at: {_mistyConfiguration.Endpoint}.");
                // Subscribe to a topic
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(_mistyConfiguration.RobotTopic + "/commands").Build());
            });
            
            _mqttClient.UseDisconnectedHandler(async e =>
            {
                await _misty.SendDebugMessageAsync("Lost connection to mqtt, reconnecting...");
                await _mqttClient.ConnectAsync(options, CancellationToken.None);
            });

            _mqttClient.UseApplicationMessageReceivedHandler(eventArgs =>
            {
                try
                {
                    string payload = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
                    dynamic payloadObj = JsonConvert.DeserializeObject(payload);
                    if (payloadObj is null)
                    {
                        throw new ArgumentException("Invalid mqtt payload provided.");
                    }
                    MqttMessageReceivedData data = new MqttMessageReceivedData();
                    data.command = payloadObj.guardian_command;
                    data.data = ((JObject)payloadObj.guardian_data).ToString(Formatting.None);
                    OnMqttMessageReceived(data);
                }
                catch (Exception e)
                {
                    _misty.SendDebugMessageAsync("Invalid mqtt message received.");
                    _misty.SendDebugMessageAsync(e.Message);
                }
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