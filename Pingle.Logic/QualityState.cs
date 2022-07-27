namespace Pingle.Logic;

public class QualityState
{
    public Queue<double?> JitterHistory { get; } = new();
    public Queue<double?> LatencyHistory { get; } = new();
    public double CurrentLatency { get; set; }
    public double CurrentJitter { get; set; }
}
