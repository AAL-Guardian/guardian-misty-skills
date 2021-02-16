using CloudConnector.Events;
using MistyRobotics.SDK.Commands;
using MistyRobotics.SDK.Events;
using MistyRobotics.SDK.Messengers;

namespace CloudConnector.Services
{
    public class MistyEventService : IMistyEventService
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

        public void OnMqttMessage(object sender, MqttMessageReceivedData data)
        {
            MistyMessageReceivedData eventdata = new MistyMessageReceivedData()
            {
                command = data.command,
                data = data.data
            };
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