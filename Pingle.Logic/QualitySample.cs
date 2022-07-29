using Pingle.Shared.Abstractions;

namespace Pingle.Shared.QualityMonitoring;

public class QualitySample : IQualitySample
{
    public double? Duration { get; set; }
    public double? Variance { get; set; }
    public double? MedianVariance { get; set; }
    public DateTime SampleTime { get; } = DateTime.UtcNow;
}
