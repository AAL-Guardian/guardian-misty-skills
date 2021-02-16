using System;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Data;

namespace CloudConnector.Services
{
    public interface IGuardianConfigurationService: IDisposable
    {
        IAsyncOperation<MistyConfiguration> GetConfigurationAsync();
    }
}