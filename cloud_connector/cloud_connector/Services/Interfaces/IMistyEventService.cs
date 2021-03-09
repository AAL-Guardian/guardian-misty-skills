using System;
using CloudConnector.Events;

namespace CloudConnector.Services.Interfaces
{
    public interface IMistyEventService: IDisposable
    {
        void StartListening();
        void OnMqttMessage(object sender, MqttMessageReceivedData data);
        event MistyMessageReceivedHandler MistyMessageReceived;
    }
}