using System.Collections.Immutable;
using System.Net;
using Microsoft.Extensions.Logging;
using Pingle.Shared.Abstractions;
using Pingle.Shared.Abstractions.Connection;
using Pingle.Shared.Events;
using Pingle.Shared.QualityMonitoring;

namespace Pingle.Logic;

public class QualityMonitorService : IQualityMonitor
{
    public EventHandler<JitterUpdatedEvent>? OnJitterUpdated { get; set; }
    public EventHandler<LatencyUpdatedEvent>? OnLatencyUpdated { get; set; }
    public EventHandler<ErrorEncounteredEvent>? OnErrorEncountered { get; set; }
    public bool IsRunning { get; private set; }
    public IReadOnlyList<IQualitySample> Samples => _qualityState.Samples.ToImmutableList();

    private readonly EndPoint _target;
    private readonly ILogger<QualityMonitorService> _logger;
    private readonly IEnumerable<IConnector> _connectors;
    private readonly ManualResetEventSlim _onWaitingForResponse = new(true);

    private QualityState _qualityState = new();
    private CancellationTokenSource _tokenSource = new();

    // todo: driven by UI
    private const int MaxSamples = 5000;

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
            _qualityState.Samples.Enqueue(new QualitySample
            {
                Duration = null,
                Variance = null
            });
        }

        if (_qualityState.Samples.Count >= MaxSamples)
        {
            _qualityState.Samples.TryDequeue(out _);
        }

        if (!_qualityState.Samples.Any())
        {
            _qualityState.Samples.Enqueue(new QualitySample
            {
                Duration = 0,
                Variance = 0
            });
        }

        var jitter = CalculateJitter();
        
        if (result.Time is not null)
        {
            _qualityState.Samples.Enqueue(new QualitySample
            {
                Duration = result.Time.Value.TotalMilliseconds ,
                Variance = Math.Abs(result.Time.Value.TotalMilliseconds - _qualityState.CurrentLatency),
                MedianVariance = jitter
            });
        }

        if (result.Time.HasValue)
        {
            _qualityState.CurrentLatency = result.Time.Value.TotalMilliseconds;
        }

        _qualityState.CurrentJitter = jitter;

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

    private double CalculateJitter()
    {
        var offset = _qualityState.Samples.Count >= 6 ? _qualityState.Samples.Count / 6 : 0;

        var samples =
            _qualityState.Samples.Where(sample => sample.Variance is not null).ToArray()[^offset..]
                .OrderBy(sample => sample.Variance).ToArray();

        if (!samples.Any())
        {
            return 0;
        }

        var middle = samples.Length / 2;
        var median = samples[samples.Length / 2].Variance;
        var even = samples.Length % 2 == 0;

        if (even)
        {
            median = (samples[middle].Variance + samples[middle - 1].Variance) / 2.0;
        }

        return median.Value;
    }
}
