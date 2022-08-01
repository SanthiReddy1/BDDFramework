namespace SnapDealTestProject.Library.Extensions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    /// DriverExtensions class
    /// </summary>
    public static partial class DriverExtensions
    {
        public static void Hover(By by) => new Actions(driver.Value).MoveToElement(WaitForElement(by)).Build().Perform();
    }
}
