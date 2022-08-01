using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Pages
{
    using OpenQA.Selenium;

    using SnapDealTestProject.Dropdowns;
    using SnapDealTestProject.Interfaces;
    using SnapDealTestProject.Library.Extensions;

    public class SearchResultsPage: ICreatablePageObject
    {
        public static By Sort = By.XPath("//div[@class='sort-drop clearfix']");
        public SortByDropdown SortByDropdown { get; } = new SortByDropdown();

        public void ApplyFilter(string Filter)
        {
            DriverExtensions.WaitForElement(Sort).Click();
            DriverExtensions.WaitForElement(By.XPath("//li[contains(.,'" + Filter + "')]")).Click();
        }
    }
}
