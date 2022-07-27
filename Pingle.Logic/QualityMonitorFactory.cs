using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pingle.Shared.Abstractions;

namespace Pingle.Logic;

public class QualityMonitorFactory : IQualityMonitorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QualityMonitorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IQualityMonitor Create(IConnectionParameters connectionParameters)
    {
        if (connectionParameters.Endpoint is IPEndPoint ipEndPoint)
        {
            return new QualityMonitorService(ipEndPoint,
                _serviceProvider.GetRequiredService<ILogger<QualityMonitorService>>(),
                _serviceProvider.GetRequiredService<IEnumerable<IConnector>>());
        }

        throw new NotImplementedException("Quality Monitor for given connection parameters does not exist");
    }
}
