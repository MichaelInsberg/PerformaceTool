namespace PerformanceTool.PerformanceTool.Tests
{
    /// <summary>
    /// Defines the Test base.
    /// </summary>
    /// <typeparam name="T">Where T can be anything</typeparam>
    public abstract class TestBase<T> : TestFixtureBase
    {
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Gets the object to test.
        /// </summary>
        protected T ObjectToTest { get; private set; }


        /// <inheritdoc />
        public override void SetUp()
        {
            base.SetUp();

            Identifier = Guid.NewGuid();
            CreateApplicationCancellationToken();
            InitializeTestObjects();
            ObjectToTest = CreateTestObject();
        }

        private void CreateApplicationCancellationToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        /// <inheritdoc />
        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();
                }

                cancellationTokenSource = null;

                var obj = ObjectToTest as IDisposable;
                obj?.Dispose();
                ObjectToTest = default;
            }
        }

        /// <summary>
        /// The object to test initialization not null.
        /// </summary>

        /// <summary>
        /// The create test object.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected abstract T CreateTestObject();

        protected virtual void InitializeTestObjects()
        {
            // 23.05.2022 MInsberg  : do nothing
        }

    }
}
