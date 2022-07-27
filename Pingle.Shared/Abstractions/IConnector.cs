namespace Pingle.Shared.Abstractions;

public interface IConnector
{
    /// <summary>
    /// Attempts to connect and measure connection metrics for given parameters
    /// </summary>
    /// <param name="parameters"><see cref="IConnectionParameters"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<IConnectionResult> TestConnection(IConnectionParameters parameters, CancellationToken cancellationToken = default);
}
