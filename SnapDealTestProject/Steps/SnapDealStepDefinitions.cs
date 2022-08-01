namespace SnapDealTestProject.Steps
{
    using NUnit.Framework;

    using SnapDealTestProject.Library.Extensions;
    using SnapDealTestProject.Pages;
    using SnapDealTestProject.Utils.Assertions;

    using TechTalk.SpecFlow;

    [Binding]
    public sealed class SnapDealStepDefinitions
    {
        HomePage homePage;

        private SearchResultsPage searchResultsPage;

        private QualityTestCase QualityTestCase;
        public SnapDealStepDefinitions(QualityTestCase QualityTestCase)
        {
            this.QualityTestCase = QualityTestCase;
        }

        [Given(@"I Open SnapDeal site")]
        public void GivenIOpenSnapDealSite()
        {
            homePage = DriverExtensions.CreatePageInstance<HomePage>();
            homePage.NavigateToApplication();
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string product)
        {
            searchResultsPage = this.homePage.HeaderComponent.EnterSearchQueryAndClickSearch<SearchResultsPage>(product);
        }

        [When(@"I apply '(.*)' SortBy filter")]
        public void WhenIApplySortByFilter(SearchSortOptions option)
        {
            this.searchResultsPage.SortByDropdown.SelectOption(option);
        }

        [Then(@"I verify '(.*)' SortBy filter is applied")]
        public void ThenIVerifySortByFilterIsApplied(SearchSortOptions option)
        {
            Assert.True(this.searchResultsPage.SortByDropdown.IsSelected(option));
        }

        [Then(@"I verify the icons displayed on the home page")]
        public void ThenIVerifyTheIconsDisplayedOnTheHomePage()
        {
            QualityCheck searchBarDisplayedCheck = new QualityCheck("Verify whether search bar is displayed");
            QualityCheck menuIconDisplayedCheck = new QualityCheck("Verify whether menu icon is not displayed");
            QualityCheck cartIconDisplayedCheck = new QualityCheck("Verify whether cart icon is displayed");
            this.QualityTestCase.AddQualityChecks(searchBarDisplayedCheck, menuIconDisplayedCheck, cartIconDisplayedCheck);
            QualityVerify.IsTrue(
                searchBarDisplayedCheck,
                this.homePage.HeaderComponent.IsSearchBarDisplayed(),
                "Search Bar is not displayed on home page");
            QualityVerify.IsFalse(
                menuIconDisplayedCheck,
                this.homePage.HeaderComponent.IsMenuIconDisplayed(),
                "Menu Icon is displayed on home page");
            QualityVerify.IsTrue(
                cartIconDisplayedCheck,
                this.homePage.HeaderComponent.IsCartIconDisplayed(),
                "Cart Icon is not displayed on home page");
        }


        [Then(@"I verify search bar is displayed")]
        public void ThenIVerifySearchBarIsDisplayed()
        {
            QualityCheck searchBarDisplayedCheck = new QualityCheck("Verify whether search bar is displayed");
            this.QualityTestCase.AddQualityChecks(searchBarDisplayedCheck);
            QualityVerify.IsTrue(
                searchBarDisplayedCheck,
                this.homePage.HeaderComponent.IsSearchBarDisplayed(),
                "Search Bar is not displayed on home page");
        }

        [Then(@"I verify menu icon displayed")]
        public void ThenIVerifyMenuIconDisplayed()
        {
            QualityCheck menuIconDisplayedCheck = new QualityCheck("Verify whether menu icon is displayed");
            this.QualityTestCase.AddQualityChecks(menuIconDisplayedCheck);
            QualityVerify.IsTrue(
                menuIconDisplayedCheck,
                this.homePage.HeaderComponent.IsMenuIconDisplayed(),
                "Menu Icon is not displayed on home page");
        }

        [Then(@"I verify cart icon is displayed")]
        public void ThenIVerifyCartIconIsDisplayed()
        {
            QualityCheck cartIconDisplayedCheck = new QualityCheck("Verify whether cart icon is displayed");
            this.QualityTestCase.AddQualityChecks(cartIconDisplayedCheck);
            QualityVerify.IsTrue(
                cartIconDisplayedCheck,
                this.homePage.HeaderComponent.IsCartIconDisplayed(),
                "Cart Icon is not displayed on home page");
        }
    }
}
