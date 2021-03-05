namespace CloudConnector.Events
{
    public delegate void MqttMessageReceivedHandler(object sender, MqttMessageReceivedData e);

    public sealed class MqttMessageReceivedData
    {
        public string command { get; set; }
        public string data { get; set; }
    }
}