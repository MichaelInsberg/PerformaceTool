using System.Collections.Concurrent;
using System.Diagnostics;
using PerformanceTool.PerformanceTool.Exceptions;

namespace PerformanceTool.PerformanceTool.Extensions;

/// <summary>
/// The concurrent dictionary extension class
/// </summary>
public static class ConcurrentDictionaryExtension
{
    private const int Delay = 10;
    private const int MaxRetries = 10;

    /// <summary>
    /// Add with retry key value to dictionary
    /// </summary>
    /// <param name="dictionary">The dictionary</param>
    /// <param name="key">The key</param>
    /// <param name="value">The value</param>
    /// <typeparam name="TKey">The generic key</typeparam>
    /// <typeparam name="TValue">The generic value</typeparam>
    /// <exception cref="ConcurrentDictionaryException"></exception>
    public static void AddWithRetry<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        var count = 0;
        while (!dictionary.TryAdd(key, value))
        {
            Task.Delay(Delay).Wait();
            count++;
            Debug.WriteLine($"Count for {nameof(RemoveWithRetry)} is {count}");
            if (count == MaxRetries)
            {
                throw new ConcurrentDictionaryException($"Max retries reached for {nameof(AddWithRetry)}");
            }
        }
    }
    
    /// <summary>
    /// Remove with retry key value from dictionary
    /// </summary>
    /// <param name="dictionary">The dictionary</param>
    /// <param name="key">The key</param>
    /// <param name="value">The value</param>
    /// <typeparam name="TKey">The generic key</typeparam>
    /// <typeparam name="TValue">The generic value</typeparam>
    /// <exception cref="ConcurrentDictionaryException"></exception>
    public static void RemoveWithRetry<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
    {
        var count = 0;
        while (!dictionary.TryRemove(key, out value))
        {
            Task.Delay(Delay).Wait();
            Debug.WriteLine($"Count for {nameof(RemoveWithRetry)} is {count}");
            count++;
            if (count == MaxRetries)
            {
                throw new ConcurrentDictionaryException($"Max retries reached for {nameof(RemoveWithRetry)}");
            }
        }
    }

}

