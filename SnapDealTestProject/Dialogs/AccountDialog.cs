using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Dialogs
{
    using OpenQA.Selenium;

    using SnapDealTestProject.Interfaces;
    using SnapDealTestProject.Library.Extensions;

    public class AccountDialog : ICreatablePageObject
    {
        public static By LoginLink = By.XPath("//a[text()='login']");

        public T OpenLoginComponent<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(LoginLink);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
