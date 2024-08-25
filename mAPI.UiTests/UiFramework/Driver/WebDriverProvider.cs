using mAPI.UiTests.Common.Models.AppSettings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace mAPI.UiTests.UiFramework.Driver;

public class WebDriverProvider
{
    public WebDriverProvider()
    {
    }

    public static IWebDriver Get()
    {
        var webDriver = GetChromeDriver();

        webDriver.Manage().Timeouts().PageLoad = WaitPeriods.PageLoad;
        webDriver.Manage().Timeouts().ImplicitWait = WaitPeriods.ImplicitWait;
        webDriver.Manage().Timeouts().AsynchronousJavaScript = WaitPeriods.ExplicitWait;

        return webDriver;
    }

    private static ChromeDriver GetChromeDriver()
    {
        var chromeOptions = new ChromeOptions
        {
            AcceptInsecureCertificates = true
        };

        chromeOptions.AddArguments("--no-sandbox");
        chromeOptions.AddUserProfilePreference("download.default_directory", Path.GetFullPath(AppSettings.Instance.BrowserSettings.DownloadsPath));


        if (AppSettings.Instance.BrowserSettings.Incognito)
        {
            chromeOptions.AddArguments("--incognito");
        }

        if (AppSettings.Instance.BrowserSettings.Headless)
        {
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--window-size=1920,1080");
        }
        else
        {
            chromeOptions.AddArgument("--start-maximized");
        }

        return new ChromeDriver(chromeOptions);
    }
}