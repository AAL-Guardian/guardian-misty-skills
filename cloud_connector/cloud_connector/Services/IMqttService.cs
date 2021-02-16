using System;
using System.Threading.Tasks;
using CloudConnector.Events;

namespace CloudConnector.Services
{
    public interface IMqttService: IDisposable
    {
        Task SendMessageAsync(string payload);
        Task Start();
        void OnMistyMessage(object sender, MistyMessageReceivedData data);
        event MqttMessageReceivedHandler MqttMessageReceived;
    }
}