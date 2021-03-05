using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Data;
using CloudConnector.Events;
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
            var caCert = X509Certificate.CreateFromCertFile("./rootCa.crt");
            var clientCert = new X509Certificate2("./certificate.cert.pfx");

            _mqttClient = new MqttClient(_mistyConfiguration.Endpoint, 8883, true, caCert, clientCert, MqttSslProtocols.TLSv1_2);

            _mqttClient.MqttMsgSubscribed += MqttMsgSubscribed;
            _mqttClient.MqttMsgPublishReceived += MqttMsgPublishReceived;
            _mqttClient.ConnectionClosed += MqttConnectionClosed;
            
            _mqttClient.Connect(_mistyConfiguration.Certificate.CertificateId);
            _mqttClient.Subscribe(new[] { _mistyConfiguration.RobotTopic }, new[] {MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            _misty.SendDebugMessageAsync($"RobotTopic: {_mistyConfiguration.RobotTopic}.");
            _misty.SendDebugMessageAsync($"Connected to AWS IoT with client id: {_mistyConfiguration.Certificate.CertificateId}.");
            return Task.CompletedTask.AsAsyncAction();
        }

        private void MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            _misty.SendDebugMessageAsync($"Successfully subscribed to topic.");
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
            _mqttClient.Publish(_mistyConfiguration.RobotTopic, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
        }
        
        private void OnMqttMessageReceived(MqttMessageReceivedData data)
        {
            MqttMessageReceived?.Invoke(this, data);
        }

        public void Dispose()
        {
            _mqttClient.Disconnect();
        }
    }
}