using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Data;
using CloudConnector.Events;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK.Messengers;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Formatter;
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
            // var cert = _mistyConfiguration.Certificate.CertificatePem + "\n" +
            //            _mistyConfiguration.Certificate.KeyPair.PrivateKey;
            // var pemData = Regex.Replace(Regex.Replace(cert, @"\s+", string.Empty), @"-+[^-]+-+", string.Empty);
            // var pemBytes = Convert.FromBase64String(pemData);
            // var privPemData = Regex.Replace(Regex.Replace(_mistyConfiguration.Certificate.KeyPair.PrivateKey, @"\s+", string.Empty), @"-+[^-]+-+", string.Empty);
            // var privPemBytes = Convert.FromBase64String(privPemData);
            // var certs = new []
            // {
            //     pemBytes,
            //     privPemBytes
            // };
            
            
            var certificate = new X509Certificate2("./certificate.pfx", string.Empty, 
                X509KeyStorageFlags.Exportable);
            // certificate.CopyWithPrivateKey(privPemBytes)
            //var rsa = new RSACryptoServiceProvider(cspParams);
            //
            // rsa.ImportCspBlob(privPemBytes);
            // rsa.PersistKeyInCsp = true;
            // certificate.PrivateKey = rsa;
            
            // Create TCP based options using the builder.
            var options = new MqttClientOptionsBuilder()
                .WithClientId(_mistyConfiguration.Certificate.CertificateId)
                .WithTcpServer(_mistyConfiguration.Endpoint, 8883)
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .WithTls(new MqttClientOptionsBuilderTlsParameters()
                {
                    UseTls = true,
                    Certificates = new []{ certificate.Export(X509ContentType.SerializedCert)},
                    AllowUntrustedCertificates = true
                })
                .Build();
            
            _mqttClient.UseConnectedHandler(async e =>
            {
                await _misty.SendDebugMessageAsync($"Connected to mqtt at: {_mistyConfiguration.Endpoint}.");
                // Subscribe to a topic
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(_mistyConfiguration.RobotTopic + "/commands").Build());
            });
            
            _mqttClient.UseDisconnectedHandler(async e =>
            {
                if (_misty.SkillStatus == NativeSkillStatus.Running)
                {
                    await _misty.SendDebugMessageAsync(e.Reason.ToString());
                    await _misty.SendDebugMessageAsync(e.Exception.Message);
                    await _misty.SendDebugMessageAsync("Lost connection to mqtt, reconnecting...");
                    await _misty.WaitAsync(5000);
                    await _mqttClient.ConnectAsync(options, CancellationToken.None);   
                }
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

            _misty.SendDebugMessageAsync($"Trying to connect to mqtt at: {_mistyConfiguration.Endpoint}.");
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

            await _misty.SendDebugMessageAsync($"Trying to connect to mqtt at: {_mistyConfiguration.Endpoint}.");
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