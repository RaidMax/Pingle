namespace Pingle.Shared.Abstractions;

public interface IQualitySample
{
    double? Duration { get; }
    double? Variance { get; }
    double? MedianVariance { get; }
    DateTime SampleTime { get; }
}
