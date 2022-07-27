using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pingle.Shared.Abstractions;

namespace Pingle.Logic.Extensions;

public static class LogicExtensions
{
    public static IServiceCollection AddQualityMonitoring(this IServiceCollection serviceCollection, IConfiguration config)
    {
        return serviceCollection.AddSingleton<IQualityMonitorFactory, QualityMonitorFactory>();
    }
}
