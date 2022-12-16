using System.Diagnostics;
using NUnit.Framework;

namespace PerformanceTool.PerformanceTool.Tests
{
    [TestFixture]
    public abstract class TestFixtureBase : IDisposable

    {
        private ConsoleTraceListener tracer;


        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        protected Guid Identifier { get; set; }

        /// <summary>
        /// Called when [time setup].
        /// </summary>
        [OneTimeSetUp]
        public virtual void OneTimeSetup()
        {
            tracer = new ConsoleTraceListener();
            _ = Trace.Listeners.Add(tracer);
        }

        /// <summary>
        /// Called when [time tear down].
        /// </summary>
        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            Trace.Flush();
            WriteDebugMessage($"OneTime teardown test {GetTestName()}");
            if (tracer != null)
            {
                Trace.Listeners.Remove(tracer);
            }
        }

        /// <summary>
        /// The test setup.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            WriteDebugMessage($"Setup test {GetTestName()}");
            Identifier = Guid.NewGuid();
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            WriteDebugMessage($"TearDown test {GetTestName()}");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(dispose: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Writes the debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteDebugMessage(string message)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// The get test name method
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>The test name</returns>
        protected static string GetTestName(bool fullName = false)
        {
            return fullName ? TestContext.CurrentContext.Test.FullName : TestContext.CurrentContext.Test.Name;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="dispose"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                tracer?.Dispose();
                tracer = null;
            }
        }
    }
}
