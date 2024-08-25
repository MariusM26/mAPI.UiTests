#nullable disable

using mAPI.UiTests.Common.Models.AppSettings;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace mAPI.UiTests.UiFramework.Driver
{
    public class Browser : AbstractWebFinder, IDisposable
    {
        private readonly IWebDriver _webDriver;
        private readonly ILogger<Browser> _logger;

        public Browser(ILogger<Browser> logger) : this(WebDriverProvider.Get())
        {
            _logger = logger;
        }

        private Browser(IWebDriver driver) : base(driver)
        {
            _webDriver = driver;
        }


        public string CurrentUrlEncoded => _webDriver.Url;


        #region Navigation
        public void Init()
        {
            GoTo(AppSettings.Instance.BrowserSettings.AppBaseUrl);
        }

        public void GoTo(string url)
        {
            _logger.LogTrace("Go to {url}", url);

            Wait(500);
            _webDriver.Navigate().GoToUrl(url);
            Wait(500);
        }

        public void Refresh()
        {
            _webDriver.Navigate().Refresh();
        }
        #endregion


        #region Wait
        public static void Wait(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }
        #endregion


        #region Setup
        public void Stop()
        {
            if (_webDriver is null)
            {
                return;
            }

            _webDriver.Close();
            _webDriver.Quit();
            _webDriver.Dispose();
        }
        #endregion


        #region Elements
        public void SendKeyboardKeys(string keys)
        {
            var builder = new Actions(_webDriver);
            var sendKey = builder.SendKeys(keys).Build();

            sendKey.Perform();
        }

        public void Hover(IWebElement element)
        {
            var action = new Actions(_webDriver);
            action.MoveToElement(element).Perform();
        }

        public void PasteClipboardText()
        {
            var builder = new Actions(_webDriver);

            builder.KeyDown(Keys.Control)
                   .SendKeys("v")
                   .KeyUp(Keys.Control)
                   .Build()
                   .Perform();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _webDriver.Quit();
            _webDriver.Dispose();
        }

        public override string GetCurrentUrl()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}