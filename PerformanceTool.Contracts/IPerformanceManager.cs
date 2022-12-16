namespace PerformanceTool.PerformanceTool.Contracts;

/// <summary>
/// The performance manager interface
/// </summary>
public interface IPerformanceManager
{
    /// <summary>
    /// Register an performance monitor tor the manager 
    /// </summary>
    /// <param name="performanceMonitor">The performance monitor to register</param>
    void RegisterMonitor(IPerformanceMonitor performanceMonitor);
    
    /// <summary>
    /// Unregister an performance monitor tor the manager 
    /// </summary>
    /// <param name="performanceMonitor">The performance monitor to unregister</param>
    void UnRegisterMonitor(IPerformanceMonitor performanceMonitor);

}

