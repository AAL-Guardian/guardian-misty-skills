using System;
using System.IO;
using System.Threading.Tasks;
using CloudConnector.Data;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK.Messengers;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Storage;
using CloudConnector.Services.Interfaces;

namespace CloudConnector.Services
{
    public sealed class GuardianConfigurationService : IGuardianConfigurationService
    {
        private readonly IRobotMessenger _misty;
        private readonly string _apiUrl;
        private readonly string _robotCode;
        private bool _resetConfig;

        private MistyConfiguration _configuration;

        public GuardianConfigurationService(IRobotMessenger misty, string apiUrl, bool resetConfig, string robotCode)
        {
            _misty = misty;
            _apiUrl = apiUrl;
            _resetConfig = resetConfig;
            _robotCode = robotCode;
        }

        public IAsyncOperation<MistyConfiguration> GetConfigurationAsync()
        {
            return GetConfigurationAsyncHelper().AsAsyncOperation();
        }

        private async Task<MistyConfiguration> GetConfigurationAsyncHelper()
        {
            if (_configuration != null)
            {
                return _configuration;
            }

            await _misty.SendDebugMessageAsync($"Getting config from: {_apiUrl}.");
            var postData = "{\"robotCode\": \"" + _robotCode + "\"}";
            var result =
                await _misty.SendExternalRequestAsync("POST", _apiUrl, null, null, postData, false, false, null, null);
            if (result.Status != ResponseStatus.Success || result.Data?.Data == null)
            {
                await _misty.SendDebugMessageAsync("Couldn't get configuration from specified endpoint.");
                // TODO throw error and try again in a couple of minutes
            }

            _configuration = JsonConvert.DeserializeObject<MistyConfiguration>(result.Data.Data as string);

            await HandleCertificateFile();
            
            await _misty.SendDebugMessageAsync("Received new config.");

            return _configuration;
        }

        private async Task HandleCertificateFile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile pfxFile = await storageFolder.CreateFileAsync("cert.pfx",CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteBytesAsync(pfxFile, Convert.FromBase64String(_configuration.Certificate.PfxBase64));

            _configuration.Certificate.PfxFile = pfxFile.Path;
        }

        public void Dispose()
        {
        }
    }
}