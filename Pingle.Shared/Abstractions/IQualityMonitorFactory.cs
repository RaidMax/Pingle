namespace Pingle.Shared.Abstractions;

public interface IQualityMonitorFactory
{
    /// <summary>
    /// Generates a new monitoring services based on the given parameters
    /// </summary>
    /// <param name="connectionParameters"><see cref="IConnectionParameters"/></param>
    /// <returns></returns>
    IQualityMonitor Create(IConnectionParameters connectionParameters);
}
