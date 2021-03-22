using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using CloudConnector.Data;
using CloudConnector.Events;
using CloudConnector.Services.Interfaces;
using M2Mqtt;
using M2Mqtt.Messages;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK.Messengers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudConnector.Services
{
    public sealed class MqttService : IMqttService
    {
        private readonly MistyConfiguration _mistyConfiguration;
        private readonly IRobotMessenger _misty;
        private MqttClient _mqttClient;

        public event MqttMessageReceivedHandler MqttMessageReceived;

        public MqttService(MistyConfiguration mistyConfiguration, IRobotMessenger misty)
        {
            _mistyConfiguration = mistyConfiguration;
            _misty = misty;
        }

        public IAsyncAction Start()
        {
            return RunStart().AsAsyncAction();
        }

        private async Task RunStart()
        {
            try
            {
                var caCert = X509Certificate.CreateFromCertFile("./rootCa.crt");
                var clientCert = X509Certificate.CreateFromCertFile(_mistyConfiguration.Certificate.PfxFile);

                _mqttClient = new MqttClient(_mistyConfiguration.Endpoint, 8883, true, caCert, clientCert,
                    MqttSslProtocols.TLSv1_2);

                _mqttClient.MqttMsgSubscribed += MqttMsgSubscribed;
                _mqttClient.MqttMsgUnsubscribed += MqttMsgUnsubscribed;
                _mqttClient.MqttMsgPublishReceived += MqttMsgPublishReceived;
                _mqttClient.ConnectionClosed += MqttConnectionClosed;

                _mqttClient.Connect(_mistyConfiguration.Certificate.CertificateId);
                SubscribeTopics();
                await _misty.SendDebugMessageAsync($"RobotTopic: {_mistyConfiguration.RobotTopic}.");
                await _misty.SendDebugMessageAsync(
                    $"Connected to AWS IoT with client id: {_mistyConfiguration.Certificate.CertificateId}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            _misty.SendDebugMessageAsync($"Successfully subscribed to topic.");
        }
        
        private void MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            _misty.SendDebugMessageAsync("Topic unsubscribed, resubscribing...");
            SubscribeTopics();
        }

        private void SubscribeTopics()
        {
            _mqttClient.Subscribe(new[] {_mistyConfiguration.RobotTopic + "/command"}, new[] {MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE});
        }

        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                _misty.SendDebugMessageAsync($"Mqtt message received from topic: {e.Topic}.");
                string payload = Encoding.UTF8.GetString(e.Message);
                dynamic payloadObj = JsonConvert.DeserializeObject(payload);
                if (payloadObj is null)
                {
                    throw new ArgumentException("Invalid mqtt payload provided.");
                }

                MqttMessageReceivedData data = new MqttMessageReceivedData();
                data.command = payloadObj.guardian_command;
                if (payloadObj.guardian_data is JObject jdata)
                    data.data = jdata.ToString(Formatting.None);
                else
                    data.data = payloadObj.guardian_data;
                OnMqttMessageReceived(data);
            }
            catch (Exception ex)
            {
                _misty.SendDebugMessageAsync("Invalid mqtt message received.");
                _misty.SendDebugMessageAsync(ex.Message);
            }
        }

        private void MqttConnectionClosed(object sender, EventArgs e)
        {
            _misty.SendDebugMessage("Lost connection to mqtt", null);
            if (_misty.SkillStatus == NativeSkillStatus.Running)
            {
                _misty.Wait(5000);
                _misty.SendDebugMessage("Reconnecting to mqtt...", null);
                _mqttClient.Connect(_mistyConfiguration.Certificate.CertificateId);
            }
        }

        public async void OnMistyMessage(object sender, MistyMessageReceivedData data)
        {
            await _misty.SendDebugMessageAsync($"Sending message.");
            _mqttClient.Publish(_mistyConfiguration.RobotTopic,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
        }

        private void OnMqttMessageReceived(MqttMessageReceivedData data)
        {
            MqttMessageReceived?.Invoke(this, data);
        }

        public void Dispose()
        {
            _mqttClient?.Disconnect();
        }
    }
}