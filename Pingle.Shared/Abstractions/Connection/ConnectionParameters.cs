using System.Net;

namespace Pingle.Shared.Abstractions.Connection;

public class ConnectionParameters : IConnectionParameters
{
    public EndPoint? Endpoint { get; set; }
    public TimeSpan? TimeOut { get; set; }
    public byte[]? Payload { get; set; }
}
