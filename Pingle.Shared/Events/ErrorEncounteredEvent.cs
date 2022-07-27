namespace Pingle.Shared.Events;

public class ErrorEncounteredEvent
{
    public Exception? Exception { get; set; }
    public string? Message { get; set; }
    public bool IsFatal { get; set; }
}
