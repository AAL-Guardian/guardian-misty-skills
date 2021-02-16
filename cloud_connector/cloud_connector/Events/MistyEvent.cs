namespace CloudConnector.Events
{
    public delegate void MistyMessageReceivedHandler(object sender, MistyMessageReceivedData e);

    public class MistyMessageReceivedData
    {
        public string command { get; set; }
        public string data { get; set; }
    }
}