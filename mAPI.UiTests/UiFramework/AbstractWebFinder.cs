using Conpend.IntegrationTests.UIFramework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace mAPI.UiTests.UiFramework.Driver
{
    public abstract class AbstractWebFinder : IWebFinder
    {
        private readonly ISearchContext _searchContext;

        protected AbstractWebFinder(ISearchContext searchContext)
        {
            _searchContext = searchContext;
        }

        public abstract string GetCurrentUrl();

        public IWebElement FindElement(By @by)
        {
            try
            {
                return _searchContext.FindElement(@by);
            }
            catch (NotFoundException ex)
            {
                throw new NoSuchElementException($"{ex.Message}. URL: {GetCurrentUrl()}");
            }
        }

        public IWebElement FindElement(By @by, TimeSpan extraTimeout)
        {
            IWebElement? webElement;
            try
            {
                webElement = GetWait(extraTimeout).Until(searchContext => searchContext.FindElement(@by));
            }
            catch (WebDriverTimeoutException)
            {
                webElement = null;
            }

            return webElement ?? FindElement(@by);
        }

        public IWebElement FindVisibleElement(By @by)
        {
            var webElement = FindElement(by);

            if (!webElement.Displayed)
            {
                throw new ElementNotVisibleException($"The element {webElement} found by {by} is not visible. URL: {GetCurrentUrl()}");
            }

            return webElement;
        }

        public IWebElement FindVisibleElement(By @by, TimeSpan extraTimeout)
        {
            IWebElement? webElement;
            try
            {
                var condition = UiConditions.ElementIsVisible(by);

                webElement = GetWait(extraTimeout).Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                webElement = null;
            }

            return webElement ?? FindVisibleElement(@by);
        }

        public bool IsElementHiddenOrMissing(By @by)
        {
            return UiConditions.InvisibilityOfElementLocated(by)(_searchContext);
        }

        public bool IsElementHiddenOrMissing(By @by, TimeSpan extraTimeout)
        {
            try
            {
                return GetWait(extraTimeout).Until(UiConditions.InvisibilityOfElementLocated(by));
            }
            catch (WebDriverTimeoutException)
            {
                return IsElementHiddenOrMissing(by);
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return _searchContext.FindElements(@by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by, TimeSpan extraTimeout)
        {
            ReadOnlyCollection<IWebElement>? webElements;
            try
            {
                webElements = GetWait(extraTimeout).Until(UiConditions.PresenceOfAllElementsLocatedBy(by));
            }
            catch (WebDriverTimeoutException)
            {
                webElements = null;
            }

            return webElements ?? FindElements(@by);
        }

        protected IWait<ISearchContext> GetWait(TimeSpan timeout)
        {
            var wait = new DefaultWait<ISearchContext>(_searchContext, new SystemClock())
            {
                Timeout = timeout,
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };

            wait.IgnoreExceptionTypes(typeof(NotFoundException));

            return wait;
        }
    }
}