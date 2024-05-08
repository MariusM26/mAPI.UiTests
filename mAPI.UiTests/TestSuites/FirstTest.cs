using mAPI.UiTests.UiFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;

namespace mAPI.UiTests.TestSuites
{
    public class FirstTest : AbstractDataTestFixture
    {
        public static By ByAttribute(string name, string value)
        {
            var byString = $".//*[@{name}]";

            if (!string.IsNullOrEmpty(value))
            {
                byString = $@".//*[@{name}=""{value}""]";
            }

            return By.XPath(byString);
        }

        [Test]
        public async Task Test()
        {
            Browser.Start();
            Browser.GoTo("http://localhost:3000");

            var browser = Browser.GetDriver();
            var inputs = browser.FindElements(By.TagName("input")).ToList();

            inputs.First().SendKeys("test@test.test");
            inputs.Skip(1).First().SendKeys("Pass123$");

            browser.FindElement(By.TagName("button")).Click();

            Browser.Wait(1500);

            var fields = browser.FindElements(By.TagName("input")).ToList();

            const string Name = "AutomatedTestsEntity3";

            fields.First().SendKeys(Name);
            fields.Skip(1).First().SendKeys("0722123123");
            browser.FindElement(ByAttribute("data-tst-component", "Test")).Click();
            browser.FindElements(By.TagName("li")).FirstOrDefault(el => el.Text == "A +ve")!.Click();

            browser.FindElement(ByAttribute("data-tst-action", "Submit")).Click();

            var newEntity = await Db.DonationDB.DCandidates.FirstOrDefaultAsync(ent => ent.FullName == Name);

            ClassicAssert.IsNotNull(newEntity, $"An entity with name '{Name}' could not be found.");

            Browser.Stop();
        }
    }
}
