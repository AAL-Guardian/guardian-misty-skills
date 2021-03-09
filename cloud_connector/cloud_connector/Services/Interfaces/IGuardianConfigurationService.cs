using System;
using Windows.Foundation;
using CloudConnector.Data;

namespace CloudConnector.Services.Interfaces
{
    public interface IGuardianConfigurationService: IDisposable
    {
        IAsyncOperation<MistyConfiguration> GetConfigurationAsync();
    }
}