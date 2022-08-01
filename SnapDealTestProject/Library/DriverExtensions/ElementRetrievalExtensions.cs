namespace SnapDealTestProject.Library.Extensions
{
    using System.Collections.Generic;

    using OpenQA.Selenium;

    /// <summary>
    /// Element Retrieval Driver Extensions
    /// </summary>
    public static partial class DriverExtensions
    {
        public static IWebElement GetElement(By by) => driver.Value.FindElement(by);

        public static IWebElement GetElement(IWebElement element, By by)
        {
            return element.FindElement(by);
        }

        public static IReadOnlyCollection<IWebElement> GetElements(By by)
        {
            return (IReadOnlyCollection<IWebElement>)driver.Value.FindElements(by);
        }

        public static IReadOnlyCollection<IWebElement> GetElements(IWebElement element, By by)
        {
            return (IReadOnlyCollection<IWebElement>)element.FindElements(by);
        }
    }
}
