using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Pingle.Shared;
using Pingle.Shared.Abstractions;
using Pingle.Shared.Exceptions;

namespace Pingle.Connections.ICMP;

public class ICMPConnection : IConnector
{
    private readonly ILogger _logger;
    private Socket? _socket;
    private readonly byte[] _buffer;

    private static readonly AddressFamily[] SupportedFamilies =
        { AddressFamily.InterNetwork, AddressFamily.InterNetworkV6 };

    public ICMPConnection(ILogger<ICMPConnection> logger)
    {
        _logger = logger;
        _buffer = new byte[1024];
    }

    public async Task<IConnectionResult> TestConnection(IConnectionParameters parameters,
        CancellationToken token = default)
    {
        if (parameters.Endpoint is null)
        {
            throw new ArgumentNullException(nameof(parameters.Endpoint), "Endpoint must be provided");
        }

        if (!SupportedFamilies.Contains(parameters.Endpoint.AddressFamily))
        {
            throw new ConnectorException("ICMP connector only supports IPv4 and IPv6");
        }

        try
        {
            if (Utilities.IsAdministrator)
            {
                return await RawSocketImplementation(parameters, token);
            }

            return await IpHelperApiImplementation((IPEndPoint)parameters.Endpoint,
                (int?)parameters.TimeOut?.TotalMilliseconds ?? 5000, token);
        }
        catch (SocketException ex) when (ex.ErrorCode == 11001)
        {
            throw new ConnectorException("Hostname is unknown");
        }
    }

    protected async Task<IConnectionResult> RawSocketImplementation(IConnectionParameters parameters,
        CancellationToken token)
    {
        Array.Clear(_buffer);

        if (_socket is null || _socket.AddressFamily != parameters.Endpoint?.AddressFamily)
        {
            _socket = new Socket(parameters.Endpoint?.AddressFamily ?? AddressFamily.InterNetwork, SocketType.Raw,
                (parameters.Endpoint?.AddressFamily ?? AddressFamily.InterNetwork) == AddressFamily.InterNetwork
                    ? ProtocolType.Icmp
                    : ProtocolType.IcmpV6);
        }

        PayloadContainer payload = new ICMPPacket(addressFamily: _socket.AddressFamily);

        using var tokenSource = new CancellationTokenSource();
        // todo: move default somewhere
        tokenSource.CancelAfter(parameters.TimeOut ?? TimeSpan.FromMilliseconds(5000));
        using var combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(tokenSource.Token, token);

        var timeSegments = new List<TimeSpan>();
        var bytesSent = 0;

        try
        {
            var start = DateTime.UtcNow;
            bytesSent = _socket.SendTo(payload.ToByteArray(), SocketFlags.None, parameters.Endpoint);
            timeSegments.Add(DateTime.UtcNow - start);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not send data to remote host");
        }

        if (bytesSent == 0)
        {
            return new ICMPResult
            {
                ResultType = ConnectionResultType.Incomplete
            };
        }

        SocketReceiveFromResult? result = null;

        try
        {
            var start = DateTime.UtcNow;
            result = await _socket.ReceiveFromAsync(_buffer, SocketFlags.None, parameters.Endpoint,
                combinedTokenSource.Token);
            timeSegments.Add(DateTime.UtcNow - start);
        }
        catch (OperationCanceledException)
        {
            // ignored (expected)
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Did not receive any data from remote host");
        }

        if (result?.ReceivedBytes > 0)
        {
            
            return new ICMPResult
            {
                ResultType = ConnectionResultType.Complete,
                Time = timeSegments[0] + timeSegments[1]
            };
        }

        return new ICMPResult
        {
            ResultType = ConnectionResultType.Incomplete
        };
    }

    protected async Task<IConnectionResult> IpHelperApiImplementation(IPEndPoint endpoint, int timeout,
        CancellationToken token)
    {
        var pingSender = new Ping();
        PingReply? result = null;

        try
        {
            // annoying that we don't have a cancellation token overload for send ping
            result = await Task.Run(
                async () => await pingSender.SendPingAsync(endpoint.Address, timeout), token);
        }
        catch (OperationCanceledException)
        {
            // ignored (expected)
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not send data to remote host");
        }

        return new ICMPResult
        {
            Time = result is null ? null : TimeSpan.FromMilliseconds(result.RoundtripTime),
            ResultType = result?.Status == IPStatus.Success
                ? ConnectionResultType.Complete
                : ConnectionResultType.Incomplete
        };
    }
}
