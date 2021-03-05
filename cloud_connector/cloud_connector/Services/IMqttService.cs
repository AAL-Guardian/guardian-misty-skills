using System;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Events;

namespace CloudConnector.Services
{
    public interface IMqttService: IDisposable
    {
        IAsyncAction Start();
        void OnMistyMessage(object sender, MistyMessageReceivedData data);
        event MqttMessageReceivedHandler MqttMessageReceived;
    }
}