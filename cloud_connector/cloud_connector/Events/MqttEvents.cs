namespace CloudConnector.Events
{
    public delegate void MqttMessageReceivedHandler(object sender, MqttMessageReceivedData e);

    public class MqttMessageReceivedData
    {
        public string command { get; set; }
        public string data { get; set; }
    }
}