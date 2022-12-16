using System.Collections.Concurrent;
using FluentAssertions;
using NUnit.Framework;
using PerformanceTool.PerformanceTool.Extensions;
#pragma warning disable CS1591

namespace PerformanceTool.PerformanceTool.Tests.Extension;

[TestFixture]
public sealed class ConcurrentDictionaryExtensionTests
{
    private ConcurrentDictionary<Guid, object> concurrentDictionary;
    private Random random;
    private const int TaskCount = 10_000;

    [SetUp]
    public void SetUp()
    {
        random = new Random();
        concurrentDictionary = new ConcurrentDictionary<Guid, object>();
    }
    
    [Test]
    public void AddWithRetry_Simple_ValidValue()
    {
        // Act
        concurrentDictionary.AddWithRetry(Guid.NewGuid(), new object());

        // Assert
        concurrentDictionary.Count.Should().Be(1);
    }

    [Test]
    public void AddWithRemove_Simple_ValidValue()
    {
        // Arrange
        var newGuid = Guid.NewGuid();
        var value = new object();
        concurrentDictionary.AddWithRetry(newGuid, value);

        concurrentDictionary.Count.Should().Be(1);
        // Act
        concurrentDictionary.RemoveWithRetry(newGuid, out var _);

        // Assert
        concurrentDictionary.Count.Should().Be(0);
    }

    [Test]
    public void AddWithRetry_MultipleTasks_ValidValue()
    {
        // Arrange
        var taskList = new List<Task>();
        for (var i = 0; i < TaskCount; i++)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Task.Delay(random.Next(0,1)).Wait();
                concurrentDictionary.AddWithRetry(Guid.NewGuid(), new object());
            });
            taskList.Add(task);
        }

        // Act
        Task.WaitAll(taskList.ToArray());

        concurrentDictionary.Count.Should().Be(TaskCount);
    }
    
    [Test]
    public void RemoveRetry_MultipleTasks_ValidValue()
    {
        // Arrange
        var taskList = new List<Task>();
        var guids = new List<Guid>();
        for (var i = 0; i < TaskCount; i++)
        {
            var newGuid = Guid.NewGuid();
            guids.Add(newGuid);
            concurrentDictionary.AddWithRetry(newGuid, new object());
        }
        concurrentDictionary.Count.Should().Be(TaskCount);

        foreach (var guid in guids)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Task.Delay(random.Next(0,1)).Wait();
                concurrentDictionary.RemoveWithRetry(guid, out var _);
            });
            taskList.Add(task);
        }

        // Act
        Task.WaitAll(taskList.ToArray());

        concurrentDictionary.Count.Should().Be(0);
    }
}

