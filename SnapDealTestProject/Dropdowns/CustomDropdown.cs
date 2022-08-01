namespace SnapDealTestProject.Dropdowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;

    using SnapDealTestProject.Interfaces;
    using SnapDealTestProject.Library.Extensions;

    /// <summary>
    /// Base drop-down object for module regression suites
    /// </summary>
    /// <typeparam name="T"> Type of Enumeration </typeparam>
    public abstract class CustomDropdown<T> : IDropdown<T>
    {
        private const string DropdownArrowLctMask =
    "//div[contains(@id,{0})]//a|//div[contains(@id,{0})]//button|//a[@id={0}]|//li[@id={0}]|//div[contains(@id,{0})]//button[@type='button']/span|//li[@id={0}]//span[@class='icon25 icon_downMenu-gray']";

        /// <summary>
        /// Get all options 
        /// </summary>
        /// <returns> Options list </returns>
        public IEnumerable<T> Options
        {
            get
            {
                this.ExpandIfNotExpanded();
                return this.OptionsFromExpandedDropdown;
            }
        }

        /// <summary>
        /// Dropdown arrow locator
        /// </summary>
        protected string DropdownArrow => DropdownArrowLctMask;

        /// <summary>
        /// Get dropdown options
        /// </summary>
        protected abstract IEnumerable<T> OptionsFromExpandedDropdown { get; }

        /// <summary>
        /// Get drop down identificator
        /// </summary>
        protected abstract IWebElement Dropdown { get; }

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option">Option to verify</param>
        /// <returns> True if option is selected, false otherwise </returns>
        public abstract bool IsSelected(T option);

        /// <inheritdoc />
        public virtual bool IsEnabled(T option)
        {
            throw new NotImplementedException("Not supported by custom dropdown");
        }

        /// <inheritdoc />
        public virtual bool IsDisplayed() => this.Dropdown.Displayed;

        /// <summary>
        /// Select option
        /// </summary>
        /// <typeparam name="TPage"> Page Type </typeparam>
        /// <param name="option"> Option to select </param>
        /// <returns> New instance of the page </returns>
        public TPage SelectOption<TPage>(T option) where TPage : ICreatablePageObject
        {
            this.SelectOption(option);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Select option
        /// </summary>
        /// <param name="option"> Option to select </param>
        public void SelectOption(T option)
        {
            this.ExpandIfNotExpanded();
            this.SelectOptionFromExpandedDropdown(option);
        }

        /// <summary>
        /// Select option
        /// </summary>
        protected abstract void SelectOptionFromExpandedDropdown(T option);

        /// <summary>
        /// Expand dropdown
        /// </summary>
        protected void ExpandIfNotExpanded()
        {
            if (!this.IsDropdownExpanded())
            {
                this.ClickDropdownArrow();
            }
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsDropdownExpanded()
        {
            string dropdownClass = DriverExtensions.GetElement(this.Dropdown, By.XPath("//ul[contains(@class,'sort-value')]")).GetAttribute("class");
            return !dropdownClass.Contains("hidden");
        }

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected virtual void ClickDropdownArrow()
        {
            this.Dropdown.Click();
        }
    }
}
