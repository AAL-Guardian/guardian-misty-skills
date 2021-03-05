using System;
using System.Collections.Generic;
using CloudConnector.Events;
using MistyRobotics.SDK.Commands;
using MistyRobotics.SDK.Events;
using MistyRobotics.SDK.Messengers;
using Newtonsoft.Json;

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
            _misty.RegisterUserEvent("guardian", GuardianUserEventHandler, 0, true, null);
        }

        private void GuardianUserEventHandler(IUserEvent response)
        {
            if (response.Source != "cloud_connector")
            {
                try
                {
                    dynamic payload = JsonConvert.DeserializeObject(response.Data["Payload"].ToString());
                    if (payload == null)
                    {
                        throw new ArgumentException("Invalid user event payload");
                    }
                    MistyMessageReceivedData eventdata = new MistyMessageReceivedData()
                    {
                        command = payload.guardian_command as string,
                        data = payload.guardian_data as string
                    };

                    OnMistyMessageReceived(eventdata);
                }
                catch (Exception e)
                {
                    _misty.SendDebugMessageAsync("Invalid user event received!");
                    _misty.SendDebugMessageAsync(e.Message);
                }  
            }
        }

        public async void OnMqttMessage(object sender, MqttMessageReceivedData data)
        {
            await _misty.TriggerEventAsync(
                "guardian",
                "cloud_connector",
                new Dictionary<string, object>()
                {
                    {"guardian_command", data.command},
                    {"guardian_data", data.data}
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