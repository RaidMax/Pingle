using System.Collections.Concurrent;
using Pingle.Shared.QualityMonitoring;

namespace Pingle.Logic;

public class QualityState
{
    public ConcurrentQueue<QualitySample> Samples { get; } = new();
    public double CurrentLatency { get; set; }
    public double CurrentJitter { get; set; }
}
