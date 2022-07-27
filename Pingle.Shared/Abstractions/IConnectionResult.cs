namespace Pingle.Shared.Abstractions;

public interface IConnectionResult
{
    /// <summary>
    /// Indicates the result of the connection attempt
    /// </summary>
    ConnectionResultType ResultType { get; }
    
    /// <summary>
    /// Round-trip period of the connection
    /// </summary>
    TimeSpan? Time { get; }
    
    /// <summary>
    /// Additional meta data about the connection result
    /// </summary>
    IDictionary<string, string>? Meta { get; }
}
