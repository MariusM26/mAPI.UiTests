using OpenQA.Selenium;

namespace mAPI.UiTests.UiFramework
{
    public static partial class Ui
    {
        #region System Bys
        public static By ByAttribute(string attributeName, string attributeValue, string? attributeExtension = null)
        {
            var xpath = string.IsNullOrEmpty(attributeValue) ? $".//*[@{attributeName}]" : $@".//*[@{attributeName}=""{attributeValue}""]";

            if (!string.IsNullOrEmpty(attributeExtension))
            {
                xpath += attributeExtension;
            }

            return By.XPath(xpath);
        }

        public static By ByPartialAttributeName(string partialAttributeName)
        {
            var xpath = $@"//*[@*[contains(name(), ""{partialAttributeName}"")]]";

            return By.XPath(xpath);
        }

        public static By ByPartialAttributeValue(string attributeName, string attributePartialValue)
        {
            return By.XPath($"//*[starts-with(@{attributeName},'{attributePartialValue}')]");
        }


        #endregion



        // Move into some extension methods
        // Sa returneze elementul
        // sa-i dau contextul
        public static By GetSibling(string tagName, string? tstAttributeType = null)
        {
            var selector = $".//following-sibling::{tagName}";
            if (tstAttributeType != null)
            {
                selector += $"[@{tstAttributeType}]";
            }
            return By.XPath(selector);
        }

        public static By GetParent(this By childBy)
        {
            // Remove (By.XPath: .//)
            var childByString = childBy.ToString()[13..];
            return By.XPath($".//*[./{childByString}]");
        }
    }
}