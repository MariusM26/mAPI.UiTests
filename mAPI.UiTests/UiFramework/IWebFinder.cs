using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace Conpend.IntegrationTests.UIFramework
{
    public interface IWebFinder : ISearchContext
    {
        string GetCurrentUrl();

        IWebElement FindElement(By @by, TimeSpan extraTimeout);

        IWebElement FindVisibleElement(By @by);

        IWebElement FindVisibleElement(By @by, TimeSpan extraTimeout);

        bool IsElementHiddenOrMissing(By @by);

        bool IsElementHiddenOrMissing(By @by, TimeSpan extraTimeout);

        ReadOnlyCollection<IWebElement> FindElements(By @by, TimeSpan extraTimeout);
    }
}