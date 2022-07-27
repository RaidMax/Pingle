namespace Pingle.Shared.Abstractions;

public enum ConnectionResultType
{
    /// <summary>
    /// The round-trip connection was completed successfully
    /// </summary>
    Complete,
    
    /// <summary>
    /// The round-trip connect did not complete sucessfully
    /// </summary>
    Incomplete
}
