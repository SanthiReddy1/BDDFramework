using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Components
{
    using OpenQA.Selenium;

    using SnapDealTestProject.Interfaces;
    using SnapDealTestProject.Library.Extensions;

    public class HeaderComponent
    {
        public static By SignIn = By.XPath("//div[contains(@class,'myAccountTab')]");
        public static By SearchInputLocator = By.Name("keyword");
        public static By SearchButtonLocator = By.XPath("//button[contains(@class,'searchformButton')]");
        public static By MenuIconLocator = By.ClassName("menuIconBar");
        public static By CartIconLocator = By.XPath("//*[contains(@class,'cartContainer')]");

        public T OpenAccountDialog<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementVisible(SignIn, 20);
            DriverExtensions.Hover(SignIn);
            return DriverExtensions.CreatePageInstance<T>();
        }

        public bool IsMenuIconDisplayed() => DriverExtensions.WaitForElement(MenuIconLocator).Displayed;

        public bool IsCartIconDisplayed() => DriverExtensions.WaitForElement(CartIconLocator).Displayed;

        public bool IsSearchBarDisplayed() => DriverExtensions.WaitForElement(SearchInputLocator).Displayed;

        /// <summary> Enter a search query and click the search button </summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="searchItems"> search item(s) to enter </param>
        /// <returns> SearchResultPage object </returns>
        public T EnterSearchQueryAndClickSearch<T>(String searchText)
            where T : ICreatablePageObject
        {
            this.EnterSearchQuery(searchText, true, false);
            return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Enter a search query
        /// </summary>
        /// <param name="text">Text to enter</param> 
        /// <param name="clearFirst">True to clear the text field first, false otherwise</param>
        /// <param name="sendSlow">The send Slow.</param>
        public void EnterSearchQuery(string text, bool clearFirst = true, bool sendSlow = true)
        {
            if (clearFirst)
            {
                DriverExtensions.WaitForElement(SearchInputLocator).Clear();
            }

            if (sendSlow)
            {
                DriverExtensions.WaitForElement(SearchInputLocator).SendKeysSlow(text);
            }
            else
            {
                DriverExtensions.WaitForElement(SearchInputLocator).SendKeys(text);
            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Clicks the search button
        /// </summary>
        /// <typeparam name="T">
        /// Object
        /// </typeparam>
        /// <returns>
        /// A new instance of the search results page
        /// </returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(SearchButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
