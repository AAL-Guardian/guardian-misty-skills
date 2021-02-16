using System;
using System.Collections.Generic;
using CloudConnector.Events;
using MistyRobotics.SDK.Commands;
using MistyRobotics.SDK.Events;
using MistyRobotics.SDK.Messengers;

namespace CloudConnector.Services
{
    public sealed class MistyEventService : IMistyEventService
    {
        private readonly IRobotMessenger _misty;
        public event MistyMessageReceivedHandler MistyMessageReceived;

        public MistyEventService(IRobotMessenger misty)
        {
            _misty = misty;
        }

        public void StartListening()
        {
            _misty.RegisterUserEvent("guardian", response =>
            {
                MistyMessageReceivedData eventdata = new MistyMessageReceivedData()
                {
                    command = response.Data["guardian-command"] as string,
                    data = response.Data["guardian-data"] as string
                };

                OnMistyMessageReceived(eventdata);
            }, 0, true, null);
        }

        public async void OnMqttMessage(object sender, MqttMessageReceivedData data)
        {
            await _misty.TriggerEventAsync(
                "Guardian",
                "cloud-connector",
                new Dictionary<string, object>()
                {
                    {"guardian-command", data.command},
                    {"guardian-data", data.data}
                },
                new List<string>());
        }

        private void OnMistyMessageReceived(MistyMessageReceivedData data)
        {
            MistyMessageReceived?.Invoke(this, data);
        }

        public void Dispose()
        {
        }
    }
}