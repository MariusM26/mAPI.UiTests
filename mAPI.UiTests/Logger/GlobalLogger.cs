using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace mAPI.UiTests.Common
{
    public class GlobalLogger(ILogger logger)
    {
        private readonly ILogger _logger = logger;

        public void SuiteStart()
        {
            try
            {
                _logger.LogTrace("[Suite-Start]");
            }
            catch
            {
                //ignore
            }
        }

        public void SuiteStartError(Exception exception)
        {
            try
            {
                _logger.LogError(exception, "[Suite-Start-Error]");
            }
            catch
            {
                //ignore
            }
        }

        public void TestStart()
        {
            try
            {
                _logger.LogTrace("Trace");
                _logger.LogDebug("debug");
                _logger.LogInformation("information");
                _logger.LogWarning("Warning");
                _logger.LogError("error");
                _logger.LogCritical("Critical");





                //_logger.LogTrace("[Test-Start] - {CurrentTestName}", CurrentTestName);
            }
            catch
            {
                //ignore
            }
        }

        public void TestStartError(Exception exception)
        {
            try
            {
                _logger.LogError(exception, $"[Test-Start-Error] - {CurrentTestName}");
            }
            catch
            {
                //ignore
            }
        }

        public void TestEnd()
        {
            try
            {
                var description = $"[Test-End] - {CurrentTestName}";

                if (CurrentTestResult != null)
                {
                    description = $"{description} - {CurrentTestResult.Outcome}";

                    if (!string.IsNullOrWhiteSpace(CurrentTestResult.Message))
                    {
                        description = $"{description}{Environment.NewLine}{CurrentTestResult.Message}";

                        if (!string.IsNullOrWhiteSpace(CurrentTestResult.StackTrace))
                        {
                            description = $"{description}{CurrentTestResult.StackTrace}";
                        }
                    }
                }

                _logger.LogTrace(message: description);
            }
            catch
            {
                //ignore
            }
        }

        public void TestEndError(Exception exception)
        {
            try
            {
                _logger.LogError(exception, "[Test-End-Error] - {CurrentTestName}", CurrentTestName);
            }
            catch
            {
                //ignore
            }
        }

        public void SuiteEnd()
        {
            try
            {
                _logger.LogTrace("[Suite-End]");
            }
            catch
            {
                //ignore
            }
        }

        public void SuiteEndError(Exception exception)
        {
            try
            {
                _logger.LogError(exception, "[Suite-End-Error]");
            }
            catch
            {
                //ignore
            }
        }

        private static string CurrentTestName => TestContext.CurrentContext.Test.Name;
        private static TestContext.ResultAdapter CurrentTestResult => TestContext.CurrentContext.Result;
    }
}