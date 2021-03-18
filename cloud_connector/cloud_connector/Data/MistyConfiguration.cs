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
        public MistyConfigurationCertificate(string certificateId, string certificatePem,
            MistyConfigurationCertificateKeyPair keyPair, string pfx)
        {
            CertificateId = certificateId;
            CertificatePem = certificatePem;
            KeyPair = keyPair;
            this.pfx = pfx;
        }

        public string CertificateId { get; }
        public string CertificatePem { get; }
        public MistyConfigurationCertificateKeyPair KeyPair { get; }
        public string pfx { get; }
    }

    public sealed class MistyConfigurationCertificateKeyPair
    {
        public MistyConfigurationCertificateKeyPair(string privateKey, string publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public string PrivateKey { get; }
        public string PublicKey { get; }
    }
}