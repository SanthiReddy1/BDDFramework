namespace SnapDealTestProject.Dropdowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OpenQA.Selenium;

    using SnapDealTestProject.Library.EnumExtensions;
    using SnapDealTestProject.Library.Extensions;

    public class SortByDropdown : CustomDropdown<SearchSortOptions>
    {
        private const string SearchOptionLctMask = "//div[contains(@class,'sorting-sec')]//li[contains(.,'{0}')]";

        private static readonly By SearchOptionsLocator = By.XPath("//ul[contains(@class,'sort-value')]");

        private static readonly By SortByDropdownLocator = By.XPath("//div[contains(@class,'sorting-sec')]");

        /// <summary>
        /// IWebElement
        /// </summary>
        protected override IWebElement Dropdown { get; } = DriverExtensions.SafeGetElement(SortByDropdownLocator);

        /// <summary>
        /// Returns list of available options
        /// </summary>
        /// <returns> List of available options </returns>
        protected override IEnumerable<SearchSortOptions> OptionsFromExpandedDropdown => DriverExtensions.GetElements(this.Dropdown, By.XPath("//li"))
            .Select(x => this.DropdownMap.First(y => y.GetNameAttribute().Equals(x.Text.Trim()))).ToList();

        private IEnumerable<SearchSortOptions> DropdownMap { get; } = Enum.GetValues(typeof(SearchSortOptions)).Cast<SearchSortOptions>();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        public override bool IsSelected(SearchSortOptions option) => option.GetEnumValue().Equals(DriverExtensions.GetElement(this.Dropdown, By.XPath("//*[@class='sort-selected']")).Text);

        /// <summary>
        /// Select option from expanded drop-down
        /// </summary>
        /// <param name="option"> Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(SearchSortOptions option) =>
            DriverExtensions.Click(By.XPath(string.Format(SearchOptionLctMask, option.GetEnumValue())));
    }
}
