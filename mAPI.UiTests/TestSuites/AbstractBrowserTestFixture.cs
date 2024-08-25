using mAPI.UiTests.Common;
using mAPI.UiTests.UiFramework;
using mAPI.UiTests.UiFramework.Driver;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAPI.UiTests.TestSuites
{
    public abstract class AbstractBrowserTestFixture : AbstractDataTestFixture
    {
        protected Browser Browser { get;  }

        private readonly GlobalLogger _globalLogger;

        protected ILogger Logger { get; }

        protected Type CurrentType { get; }

        public AbstractBrowserTestFixture()
        {
            Browser = Resolve<Browser>();

            CurrentType = GetType();

            Logger = (ILogger)Resolve(typeof(ILogger<>).MakeGenericType(CurrentType));

            _globalLogger = new GlobalLogger(Logger);

        }

        [SetUp]
        public void TestStart()
        {
            _globalLogger.SuiteStart();
            _globalLogger.TestStart();

            try
            {
                TestContext.Out.WriteLine($"Ran from {GetType().Name} suite");
            }
            catch (Exception ex)
            {
                _globalLogger.TestStartError(ex);
                throw;
            }
        }

        [TearDown]
        public async Task TestEnd()
        {
            _globalLogger.TestEnd();

            try
            {
                await OnTestEnd();
                TestContext.Out.WriteLine($"Finished {GetType().Name} suite");
            }
            catch (Exception ex)
            {
                _globalLogger.TestStartError(ex);
                throw;
            }
        }

        [OneTimeTearDown]
        public async Task SuiteEnd()
        {
            _globalLogger.SuiteEnd();

            try
            {
                await OnSuiteEnd();
            }
            catch (Exception ex)
            {
                _globalLogger.SuiteEndError(ex);
                throw;
            }
        }

        protected virtual Task OnTestEnd() => Task.CompletedTask;
        protected virtual Task OnSuiteEnd() => Task.CompletedTask;
    }
}