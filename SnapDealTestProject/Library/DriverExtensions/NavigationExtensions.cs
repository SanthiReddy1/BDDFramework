using System;

namespace SnapDealTestProject.Library.Extensions
{
    public partial class DriverExtensions
    {
        public static void GoToUrl(String url)
        {
            driver.Value.Navigate().GoToUrl(url);
        }

        public static void Refresh()
        {
            driver.Value.Navigate().Refresh();
        }
    }
}
