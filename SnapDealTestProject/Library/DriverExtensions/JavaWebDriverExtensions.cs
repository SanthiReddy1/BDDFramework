using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Library.Extensions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public static partial class DriverExtensions
    {
        public static IJavaScriptExecutor Scripts() => (IJavaScriptExecutor)driver.Value;

        public static bool WaitForPageLoad(int timeoutInMilliseconds = 30000)
        {
            try
            {
                new WebDriverWait(driver.Value, TimeSpan.FromMilliseconds((double)timeoutInMilliseconds)).Until<bool>((Func<IWebDriver, bool>)(x => (string)Scripts().ExecuteScript("return document.readyState;") == "complete"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool WaitForJavaScript(int timeoutInMilliseconds = 30000)
        {
            try
            {
                new WebDriverWait(driver.Value, TimeSpan.FromMilliseconds((double)timeoutInMilliseconds)).Until<bool>((Func<IWebDriver, bool>)(x => (bool)Scripts().ExecuteScript("if ((window.jQuery && window.jQuery.active) || (window.Cobalt_Testing_Automation && window.Cobalt_Testing_Automation.ActiveRequests)) {return false; } else {return true}")));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool WaitForAnimation(this IWebDriver driver, int timeoutInMilliseconds = 30000)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromMilliseconds((double)timeoutInMilliseconds)).Until<bool>((Func<IWebDriver, bool>)(x => (bool)Scripts().ExecuteScript("return ($(':animated').size() < 1);")));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
