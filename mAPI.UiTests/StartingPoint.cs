using mAPI.UiTests.Common;
using NUnit.Framework;

namespace mAPI.UiTests
{
    /// <summary>
    /// Global class to initialize IoC as a one time setup.
    /// </summary>
    [SetUpFixture]
    public class StartingPoint
    {
        [OneTimeSetUp]
        public Task GlobalSetup()
        {
            IoC.Init();

            return Task.CompletedTask;
        }

        [OneTimeTearDown]
        public Task GlobalTeardown()
        {
            return Task.CompletedTask;
        }
    }
}
