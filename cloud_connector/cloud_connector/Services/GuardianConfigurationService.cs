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
        private readonly StorageFolder _storageFolder = ApplicationData.Current.LocalFolder;

        private MistyConfiguration _configuration;

        public GuardianConfigurationService(IRobotMessenger misty, string apiUrl, string robotCode)
        {
            _misty = misty;
            _apiUrl = apiUrl;
            _robotCode = robotCode;
        }

        public IAsyncOperation<MistyConfiguration> GetConfigurationAsync()
        {
            return GetConfigurationAsyncHelper().AsAsyncOperation();
        }
        
        public IAsyncAction ResetConfigurationAsync()
        {
            return ResetConfiguration().AsAsyncAction();
        }

        private async Task ResetConfiguration()
        {
            StorageFile configFile = (await _storageFolder.TryGetItemAsync("config.json")) as StorageFile;
            if (configFile != null) // if config exists on misty then get that, else get from cloud
            {
                await _misty.SendDebugMessageAsync("Deleting Configuration...");
                await configFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                await _misty.SendDebugMessageAsync("Config deleted.");
            }
        }

        private async Task<MistyConfiguration> GetConfigurationAsyncHelper()
        {
            if (_configuration != null)
            {
                return _configuration;
            }

            StorageFile configFile = (await _storageFolder.TryGetItemAsync("config.json")) as StorageFile;
            if (configFile != null) // if config exists on misty then get that, else get from cloud
            {
                await _misty.SendDebugMessageAsync("Found existing config file, use that.");
                string config = await FileIO.ReadTextAsync(configFile);
                _configuration = JsonConvert.DeserializeObject<MistyConfiguration>(config);   
            }
            else
            {
                await _misty.SendDebugMessageAsync($"No config file found locally, getting config from: {_apiUrl}.");
                var postData = "{\"robotCode\": \"" + _robotCode + "\"}";
                var result =
                    await _misty.SendExternalRequestAsync("POST", _apiUrl, null, null, postData, false, false, null, null);
                if (result.Status != ResponseStatus.Success || result.Data?.Data == null)
                {
                    await _misty.SendDebugMessageAsync("Couldn't get configuration from specified endpoint.");
                    // TODO throw error and try again in a couple of minutes
                }
                
                await _misty.SendDebugMessageAsync($"Saving config file locally...");
                configFile = await _storageFolder.CreateFileAsync("config.json",CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(configFile, result.Data.Data as string);

                _configuration = JsonConvert.DeserializeObject<MistyConfiguration>(result.Data.Data as string);   
            }

            await HandleCertificateFile();
            
            await _misty.SendDebugMessageAsync("Got config.");

            return _configuration;
        }

        private async Task HandleCertificateFile()
        {
            StorageFile pfxFile = await _storageFolder.CreateFileAsync("cert.pfx",CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteBytesAsync(pfxFile, Convert.FromBase64String(_configuration.Certificate.PfxBase64));

            _configuration.Certificate.PfxFile = pfxFile.Path;
        }

        public void Dispose()
        {
        }
    }
}