using System;
using System.Threading.Tasks;
using CloudConnector.Data;

namespace CloudConnector.Services
{
    public interface IGuardianConfigurationService: IDisposable
    {
        Task<MistyConfiguration> GetConfigurationAsync();
    }
}