using System.Net;

namespace Pingle.Shared.Abstractions;

public interface IConnectionParameters
{
    /// <summary>
    /// Remote endpoint to connect to
    /// </summary>
    EndPoint? Endpoint { get; }
    
    /// <summary>
    /// Period before abandoning connection attempt
    /// </summary>
    TimeSpan? TimeOut { get; }
    
    /// <summary>
    /// Optional prebuilt payload
    /// </summary>
    byte[]? Payload { get; }
}
