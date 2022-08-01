namespace SnapDealTestProject.Library.Extensions
{
    using System;

    using OpenQA.Selenium;

    /// <summary>
    /// DriverExtensions class
    /// </summary>
    public partial class DriverExtensions
    {
        public static void Click(By elementBy)
        {
            try
            {
                DriverExtensions.WaitForElement(elementBy).Click();
            }
            catch (InvalidOperationException)
            {
                ScrollIntoView(elementBy);
                driver.Value.FindElement(elementBy).Click();
            }
        }
    }
}