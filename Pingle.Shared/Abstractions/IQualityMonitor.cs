using Pingle.Shared.Events;

namespace Pingle.Shared.Abstractions;

public interface IQualityMonitor : IControllableService
{
    /// <summary>
    /// Fires when jitter result is received
    /// </summary>
    EventHandler<JitterUpdatedEvent>? OnJitterUpdated { get; set; }
    
    /// <summary>
    /// Fires when latency result is received
    /// </summary>
    EventHandler<LatencyUpdatedEvent>? OnLatencyUpdated { get; set; }
    
    /// <summary>
    /// Fires when a monitoring error occurs
    /// </summary>
    EventHandler<ErrorEncounteredEvent>? OnErrorEncountered { get; set; }
    
    /// <summary>
    /// Sets how often the monitor polls for connection quality
    /// </summary>
    /// <param name="interval"><see cref="TimeSpan"/></param>
    void SetPollingInterval(TimeSpan interval);
    
    /// <summary>
    /// Collection of samples
    /// </summary>
    IReadOnlyList<IQualitySample> Samples { get; }
}
