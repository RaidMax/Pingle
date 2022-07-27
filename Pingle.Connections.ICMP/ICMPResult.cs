using Pingle.Shared.Abstractions;

namespace Pingle.Connections.ICMP;

public class ICMPResult : IConnectionResult
{
    public ConnectionResultType ResultType { get; set; }
    public TimeSpan? Time { get; set; }
    public IDictionary<string, string>? Meta { get; set; }
}
