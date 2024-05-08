using mAPI.UiTests.Common;
using NUnit.Framework;

namespace mAPI.UiTests
{
    [SetUpFixture]
    public class Global
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
