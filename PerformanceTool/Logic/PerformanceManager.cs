using System.Collections.Concurrent;
using PerformanceTool.PerformanceTool.Contracts;
using PerformanceTool.PerformanceTool.Extensions;

namespace PerformanceTool.PerformanceTool.Logic;

public sealed class PerformanceManager : IPerformanceManager
{
    private readonly ConcurrentDictionary<Guid, IPerformanceMonitor> monitors;

    /// <summary>
    /// Create a new instance of PerformanceManager
    /// </summary>
    public PerformanceManager()
    {
        monitors = new ConcurrentDictionary<Guid, IPerformanceMonitor>();
    }
    
    /// <inheritdoc />
    public void RegisterMonitor(IPerformanceMonitor performanceMonitor)
    {
        _ = performanceMonitor ?? throw new ArgumentNullException(nameof(performanceMonitor));
        monitors.AddWithRetry(performanceMonitor.Identifier, performanceMonitor);
    }

    /// <inheritdoc />
    public void UnRegisterMonitor(IPerformanceMonitor performanceMonitor)
    {
        _ = performanceMonitor ?? throw new ArgumentNullException(nameof(performanceMonitor));
        monitors.RemoveWithRetry(performanceMonitor.Identifier, out var _);
    }
}

