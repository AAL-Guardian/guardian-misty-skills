using System;
using System.IO;
using System.Threading.Tasks;
using CloudConnector.Data;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK.Messengers;
using Newtonsoft.Json;

namespace CloudConnector.Services
{
    public class GuardianConfigurationService : IGuardianConfigurationService
    {
        private readonly IRobotMessenger _misty;
        private readonly string _apiUrl;

        private MistyConfiguration _configuration;
        
        public GuardianConfigurationService(IRobotMessenger misty, string apiUrl)
        {
            _misty = misty;
            _apiUrl = apiUrl;
        }

        public async Task<MistyConfiguration> GetConfigurationAsync()
        {
            if (_configuration != null)
            {
                return _configuration;
            }
            
            string directory = "C:\\cloud-connector";
            string configFileName = "config.json";
            string fullpath = Path.Combine(directory, configFileName);
            
            Directory.CreateDirectory(directory);
            
            if (!File.Exists(fullpath))
            {
                var result = await _misty.SendExternalRequestAsync("GET", _apiUrl, "", null, null, false, false, null, null);

                if (result.Status != ResponseStatus.Success || result.Data?.Data == null)
                {
                    // TODO throw exception?
                    // result.ErrorMessage
                }
                
                _configuration = JsonConvert.DeserializeObject<MistyConfiguration>(result.Data.Data as string);

                await File.WriteAllTextAsync(fullpath,JsonConvert.SerializeObject(_configuration));
            }
            else
            {
                _configuration =
                    JsonConvert.DeserializeObject<MistyConfiguration>(
                        await File.ReadAllTextAsync(fullpath)
                        );
            }

            return _configuration;
        }

        public void Dispose()
        {
        }
    }
}