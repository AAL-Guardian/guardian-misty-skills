using System;
using Windows.Foundation;
using CloudConnector.Events;

namespace CloudConnector.Services.Interfaces
{
    public interface IMqttService: IDisposable
    {
        IAsyncAction Start();
        void OnMistyMessage(object sender, string data);
        event MqttMessageReceivedHandler MqttMessageReceived;
    }
}