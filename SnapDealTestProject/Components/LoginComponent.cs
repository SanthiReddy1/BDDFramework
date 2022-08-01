using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Components
{
    using System.Configuration;
    using System.Threading;

    using OpenQA.Selenium;

    using SnapDealTestProject.Interfaces;
    using SnapDealTestProject.Library.Extensions;

    public class LoginComponent : ICreatablePageObject
    {
        private readonly string Username = ConfigurationManager.AppSettings["Username"];
        private readonly string Password = ConfigurationManager.AppSettings["Password"];
        public static By LoginEmail = By.XPath("//input[@type='email']");
        public static By Continue = By.XPath("//*[text()='Next']");
        public static By LoginPassword = By.Name("password");
        public static By Login = By.Id("submitLoginUC");
        public static By LoginFrame = By.XPath("//iframe[@name='iframeLogin']");
        public static By GmailButton = By.XPath("//*[contains(@class,'gmLogin')]");

        public void EnterUserNamePasswordAndClickSignOn()
        {
            DriverExtensions.WaitForElementClickable(LoginFrame);
            DriverExtensions.SwitchToFrame(LoginFrame);
            DriverExtensions.Click(GmailButton);
            DriverExtensions.SwitchToNewWindow();
            DriverExtensions.WaitForElementVisible(LoginEmail, 10);
            DriverExtensions.WaitForElement(LoginEmail).SendKeys(this.Username);
            DriverExtensions.Click(Continue);
            DriverExtensions.WaitForElement(LoginPassword).SendKeys(this.Password);
            DriverExtensions.Click(Login);
            DriverExtensions.SwitchToFrame();
        }
    }
}
