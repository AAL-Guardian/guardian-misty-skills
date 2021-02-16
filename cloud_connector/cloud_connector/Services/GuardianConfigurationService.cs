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

        private MistyConfiguration _configuration;
        
        public GuardianConfigurationService(IRobotMessenger misty, string apiUrl)
        {
            _misty = misty;
            _apiUrl = apiUrl;
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
            
            string directoryName = "cloud-connector";
            string configFileName = "config.json";

            var storageLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Documents);
            StorageFolder folder;
            if (await storageLibrary.SaveFolder.TryGetItemAsync(directoryName) == null)
                folder = await storageLibrary.SaveFolder.CreateFolderAsync(directoryName);
            else
                folder = await storageLibrary.SaveFolder.GetFolderAsync(directoryName);
            var storageFile = await folder.TryGetItemAsync(configFileName);
            
            if (storageFile == null)
            {
                var result = await _misty.SendExternalRequestAsync("GET", _apiUrl, null, null, null, false, false, null, null);

                await _misty.SendDebugMessageAsync(result.ResponseType.ToString());
                if (result.Status != ResponseStatus.Success || result.Data?.Data == null)
                {
                    // TODO throw exception?
                    // result.ErrorMessage
                }
                
                _configuration = JsonConvert.DeserializeObject<MistyConfiguration>(result.Data.Data as string);

                var file = await folder.CreateFileAsync(configFileName);
                await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(_configuration));
            }
            else
            {
                _configuration =
                    JsonConvert.DeserializeObject<MistyConfiguration>(
                        await FileIO.ReadTextAsync((StorageFile)storageFile)
                        );
            }

            return _configuration;
        }

        public void Dispose()
        {
        }
    }
}