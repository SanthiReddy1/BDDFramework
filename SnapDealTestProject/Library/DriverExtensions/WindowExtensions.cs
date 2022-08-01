namespace SnapDealTestProject.Library.Extensions
{
    using System.Linq;

    using OpenQA.Selenium;

    /// <summary>
    /// DriverExtensions class
    /// </summary>
    public partial class DriverExtensions
    {
        public static void ScrollIntoView(By by, int offset = 0)
        {
            int num = driver.Value.FindElement(by).Location.Y - offset;
            ((IJavaScriptExecutor)driver.Value).ExecuteScript("window.scrollTo(0," + (object)num + ")");
        }

        public static void SwitchToFrame(By elementLocator = null)
        {
            if (elementLocator != null)
            {
                driver.Value.SwitchTo().Frame(WaitForElement(elementLocator));
            }
            else
            {
                driver.Value.SwitchTo().DefaultContent();
            }
        }

        public static void SwitchToNewWindow()
        {
            var windowHandles = driver.Value.WindowHandles;
            driver.Value.SwitchTo().Window(windowHandles.Last());
        }
    }
}
