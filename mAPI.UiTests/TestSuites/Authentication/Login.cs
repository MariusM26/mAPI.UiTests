using mAPI.UiTests.UiFramework;
using mAPI.UiTests.UiFramework.Driver;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;

namespace mAPI.UiTests.TestSuites.Authentication
{
    public class Login : AbstractDataTestFixture
    {
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

            // de aici

            var firstField = browser.FindElement(Ui.ByAttribute("data-tst-test", "Marus1"));

            var sibling = firstField.FindElement(Ui.GetSibling("div"));

            var iduLuFracso = sibling.GetAttribute("data-tst-test");


            // pana aici

            var fields = browser.FindElements(By.TagName("input")).ToList();

            const string Name = "AutomatedTestsEntity3";

            fields.First().SendKeys(Name);
            fields.Skip(1).First().SendKeys("0722123123");
            browser.FindElement(Ui.ByAttribute("data-tst-component", "Test")).Click();
            browser.FindElements(By.TagName("li")).FirstOrDefault(el => el.Text == "A +ve")!.Click();

            browser.FindElement(Ui.ByAttribute("data-tst-action", "Submit")).Click();

            var newEntity = await Db.DonationDB.DCandidates.FirstOrDefaultAsync(ent => ent.FullName == Name);

            ClassicAssert.IsNotNull(newEntity, $"An entity with name '{Name}' could not be found.");

            Browser.Stop();
        }
    }
}
