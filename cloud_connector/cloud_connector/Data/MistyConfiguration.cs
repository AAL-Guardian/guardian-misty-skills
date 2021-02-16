using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudConnector.Data
{
    public record MistyConfiguration
    {
        public string ClientId { get; }
        public string Endpoint { get; }
        public string RobotCode { get; }
        public string RobotTopic { get; }
        public string Token { get; }

        public MistyConfiguration(string clientId, string endpoint, string robotCode, string robotTopic, string token) => 
            (ClientId, Endpoint, RobotCode, RobotTopic, Token) = (clientId, endpoint, robotCode, robotTopic, token);
    }
}