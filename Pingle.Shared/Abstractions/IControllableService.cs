namespace Pingle.Shared.Abstractions;

public interface IControllableService : IDisposable
{
    /// <summary>
    /// Starts/initializes the controllable service
    /// </summary>
    void Start();
    
    /// <summary>
    /// Stops/disposes the initialized service
    /// </summary>
    void Stop();
    
    /// <summary>
    /// Indicates if the service is running
    /// </summary>
    bool IsRunning { get; }
}
