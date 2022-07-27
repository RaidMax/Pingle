using System.Net;
using Microsoft.Extensions.Logging;
using Pingle.Shared.Abstractions;
using Pingle.Shared.Abstractions.Connection;
using Pingle.Shared.Events;

namespace Pingle.Logic;

public class QualityMonitorService : IQualityMonitor
{
    public EventHandler<JitterUpdatedEvent>? OnJitterUpdated { get; set; }
    public EventHandler<LatencyUpdatedEvent>? OnLatencyUpdated { get; set; }
    public EventHandler<ErrorEncounteredEvent>? OnErrorEncountered { get; set; }
    public bool IsRunning { get; private set; }

    private readonly EndPoint _target;
    private readonly ILogger<QualityMonitorService> _logger;
    private readonly IEnumerable<IConnector> _connectors;
    private readonly ManualResetEventSlim _onWaitingForResponse = new(true);

    private QualityState _qualityState = new();
    private CancellationTokenSource _tokenSource = new();

    // todo: driven by UI
    private const int MaxJitterItems = 25;

    public QualityMonitorService(EndPoint target, ILogger<QualityMonitorService> logger,
        IEnumerable<IConnector> connectors)
    {
        _target = target;
        _logger = logger;
        _connectors = connectors;
    }

    private Timer? _monitorTimer;

    public void Dispose()
    {
        _monitorTimer?.Dispose();
        _tokenSource.Dispose();
    }

    public void Start()
    {
        _qualityState = new QualityState();
        _monitorTimer = new Timer(OnTimerTick);
        IsRunning = true;
    }

    // todo: conditional sync/vs async to see if there's a non-trivial difference
    private async void OnTimerTick(object? state)
    {
        if (!IsRunning)
        {
            return;
        }

        if (!_onWaitingForResponse.IsSet)
        {
            _logger.LogDebug("Previous monitor event is not yet finished");
            return;
        }

        _onWaitingForResponse.Wait();
        _onWaitingForResponse.Reset();

        var connector = _connectors.First();
        IConnectionResult result;

        try
        {
            result = await connector.TestConnection(new ConnectionParameters
            {
                Endpoint = _target
            }, _tokenSource.Token);

            // it's possible for the connection implementation to not throw an task cancelled exception
            if (!IsRunning || _tokenSource.IsCancellationRequested)
            {
                return;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get result of connection");
            OnErrorEncountered?.Invoke(this, new ErrorEncounteredEvent
            {
                Exception = ex,
                Message = ex.Message,
                IsFatal = true
            });
            return;
        }

        if (result.ResultType == ConnectionResultType.Incomplete)
        {
            // logic for packet loss
        }

        if (_qualityState.JitterHistory.Count >= MaxJitterItems)
        {
            _qualityState.JitterHistory.Dequeue();
        }

        if (!_qualityState.JitterHistory.Any())
        {
            _qualityState.JitterHistory.Enqueue(0);
        }

        if (result.Time is not null)
        {
            _qualityState.JitterHistory.Enqueue(Math.Abs(result.Time.Value.Milliseconds -
                                                         _qualityState.CurrentLatency));
        }

        if (result.Time.HasValue)
        {
            _qualityState.CurrentLatency = result.Time.Value.TotalMilliseconds;
        }

        if (_qualityState.JitterHistory.Any())
        {
            _qualityState.CurrentJitter = GetMedianJitter() ?? 0;
        }

        OnLatencyUpdated?.Invoke(this, new LatencyUpdatedEvent
        {
            Latency = _qualityState.CurrentLatency
        });

        OnJitterUpdated?.Invoke(this, new JitterUpdatedEvent
        {
            Jitter = _qualityState.CurrentJitter
        });

        _onWaitingForResponse.Set();
    }

    public void Stop()
    {
        if (!IsRunning)
        {
            return;
        }

        IsRunning = false;
        _monitorTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        _tokenSource.Cancel();
        _tokenSource.Dispose();
        _tokenSource = new CancellationTokenSource();
    }

    public void SetPollingInterval(TimeSpan interval)
    {
        if (interval <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(interval), "Polling interval must be greater or equal to 0");
        }

        _monitorTimer?.Change(TimeSpan.Zero, interval);
    }

    private double? GetMedianJitter()
    {
        var even = _qualityState.JitterHistory.Count % 2 == 0;
        var ordered = _qualityState.JitterHistory.OrderByDescending(c => c).ToList();
        var median = ordered[_qualityState.JitterHistory.Count / 2];

        if (even)
        {
            median =
                (ordered[_qualityState.JitterHistory.Count / 2] + ordered[_qualityState.JitterHistory.Count / 2 - 1]) /
                2.0;
        }

        return median;
    }
}
