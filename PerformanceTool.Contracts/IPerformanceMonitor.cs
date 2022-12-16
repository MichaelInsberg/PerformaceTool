namespace PerformanceTool.PerformanceTool.Contracts;

/// <summary>
/// The performance monitor interface
/// </summary>
public interface IPerformanceMonitor
{
    /// <summary>
    /// The identifier for the performance monitor
    /// </summary>
    Guid Identifier { get; }
}
