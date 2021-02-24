using System;
using System.IO;
using System.Threading.Tasks;
using CloudConnector.Data;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK.Messengers;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Storage;

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
            
            string directoryName = "cloud_connector";
            string configFileName = "config.json";

            var storageLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Documents);
            StorageFolder folder;
            if (await storageLibrary.SaveFolder.TryGetItemAsync(directoryName) == null)
                folder = await storageLibrary.SaveFolder.CreateFolderAsync(directoryName);
            else
                folder = await storageLibrary.SaveFolder.GetFolderAsync(directoryName);
            var storageFile = await folder.TryGetItemAsync(configFileName);

            if (_resetConfig)
            {
                await _misty.SendDebugMessageAsync("Resetting config...");
                await storageFile.DeleteAsync();
                _resetConfig = false;
                storageFile = null;
            }
            
            if (storageFile == null)
            {
                await _misty.SendDebugMessageAsync($"Getting config from: {_apiUrl}.");
                var postData = "{\"robotCode\": \"" + _robotCode + "\"}";
                var result = await _misty.SendExternalRequestAsync("POST", _apiUrl, null, null, postData, false, false, null, null);
                if (result.Status != ResponseStatus.Success || result.Data?.Data == null)
                {
                    await _misty.SendDebugMessageAsync("Couldn't get configuration from specified endpoint.");
                    // TODO throw error and try again in a couple of minutes
                }
                
                _configuration = JsonConvert.DeserializeObject<MistyConfiguration>(result.Data.Data as string);

                var file = await folder.CreateFileAsync(configFileName);
                await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(_configuration));
                await _misty.SendDebugMessageAsync("Received new config.");
            }
            else
            {
                await _misty.SendDebugMessageAsync("Getting config from local storage.");
                _configuration =
                    JsonConvert.DeserializeObject<MistyConfiguration>(
                        await FileIO.ReadTextAsync((StorageFile)storageFile)
                        );
            }

            await _misty.SendDebugMessageAsync(JsonConvert.SerializeObject(_configuration, Formatting.Indented));
            return _configuration;
        }

        public void Dispose()
        {
        }
    }
}