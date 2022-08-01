namespace SnapDealTestProject.Library.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// DriverExtensions class
    /// </summary>
    public partial class DriverExtensions
    {
        private static int currentImplicitWait = 30000;

        public static void SetTimeout(int waitInMilliSeconds) => driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds((double)waitInMilliSeconds);

        public static void ResetTimeout() => driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds((double)currentImplicitWait);

        public static void DisableTimeout() => driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(0.0);

        public static void WaitForElementVisible(By locator, int waitInSeconds = 5)
        {
            WebDriverWait w = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(waitInSeconds));
            w.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitForElementClickable(By locator, int waitInSeconds = 5)
        {
            WebDriverWait w = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(waitInSeconds));
            w.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitForCondition(Func<object, bool> condition, int timeOut = 30)
        {
            var wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(timeOut));
            wait.Until(condition);
        }

        public static IWebElement WaitForElement(By by, int timeOut = 30000)
        {
            IWebElement element = (IWebElement)null;
            ExecuteWithoutImplicitTimeout(
                (Action)(() => 
                                { 
                                    element = timeOut > 0
                                                  ? new WebDriverWait(driver.Value, TimeSpan.FromMilliseconds((double)timeOut)).Until<IWebElement>((Func<IWebDriver, IWebElement>)(d => d.FindElement(by)))
                                                  : driver.Value.FindElement(by);
                                }));
            return element;
        }

        private static void ExecuteWithoutImplicitTimeout(Action action)
        {
            if (action == null)
                return;
            try
            {
                DisableTimeout();
                action();
            }
            finally
            {
                SetTimeout(currentImplicitWait);
            }
        }

        /// <summary>Safe Get Element</summary>
        /// <param name="driver">driver</param>
        /// <param name="locator">The locator.</param>
        /// <returns>result element</returns>
        public static IWebElement SafeGetElement(By locator)
        {
            IReadOnlyCollection<IWebElement> temp = driver.Value.FindElements(locator);
            return temp.Count > 0 ? temp.First() : null;
        }
    }
}
