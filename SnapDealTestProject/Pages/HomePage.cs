namespace SnapDealTestProject.Pages
{
    using System.Configuration;
    using System.Threading;

    using OpenQA.Selenium;

    using SnapDealTestProject.Components;
    using SnapDealTestProject.Dialogs;
    using SnapDealTestProject.Interfaces;
    using SnapDealTestProject.Library.Extensions;

    public class HomePage : ICreatablePageObject
    {
        private readonly string Url = ConfigurationManager.AppSettings["AppUrl"];

        public HeaderComponent HeaderComponent { get; } = new HeaderComponent();

        public void NavigateToApplication()
        {
            DriverExtensions.GoToUrl(this.Url);
            DriverExtensions.WaitForPageLoad();
        }

        public void LoginToTheApplication()
        {
            DriverExtensions.GoToUrl(this.Url);
            var accountDialog = HeaderComponent.OpenAccountDialog<AccountDialog>();
            var loginComponent = accountDialog.OpenLoginComponent<LoginComponent>();
            loginComponent.EnterUserNamePasswordAndClickSignOn();
        }
    }
}
