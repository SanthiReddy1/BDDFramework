using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Library.Extensions
{
    using System.Globalization;
    using System.Threading;

    using OpenQA.Selenium;

    public static partial class DriverExtensions
    {
        public static void SendKeysSlow(this IWebElement element, string text)
        {
            foreach (char ch in text)
            {
                element.SendKeys(ch.ToString((IFormatProvider)CultureInfo.InvariantCulture));
                Thread.Sleep(100);
                DriverExtensions.WaitForJavaScript();
            }

            DriverExtensions.WaitForJavaScript();
        }

        public static string GetText(By by)
        {
            return DriverExtensions.WaitForElement(by).Text.Trim();
        }
    }
}
