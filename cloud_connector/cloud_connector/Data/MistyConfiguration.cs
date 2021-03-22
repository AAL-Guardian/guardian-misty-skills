namespace CloudConnector.Data
{
    public sealed class MistyConfiguration
    {
        public string ClientId { get; }
        public string Endpoint { get; }
        public string RobotCode { get; }
        public string RobotTopic { get; }
        public string Token { get; }
        public MistyConfigurationCertificate Certificate { get; }


        public MistyConfiguration(string clientId, string endpoint, string robotCode, string robotTopic, string token,
            MistyConfigurationCertificate certificate)
        {
            Certificate = certificate;
            (ClientId, Endpoint, RobotCode, RobotTopic, Token) = (clientId, endpoint, robotCode, robotTopic, token);
        }
    }

    public sealed class MistyConfigurationCertificate
    {
        public MistyConfigurationCertificate(string certificateId, string pfxUrl, string pfxBase64)
        {
            CertificateId = certificateId;
            PfxUrl = pfxUrl;
            PfxBase64 = pfxBase64;
        }

        public string CertificateId { get; }
        public string PfxUrl { get; }
        public string PfxFile { get; set; }
        public string PfxBase64 { get; }
        
    }
}