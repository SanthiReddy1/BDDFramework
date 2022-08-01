namespace SnapDealTestProject
{
    using SnapDealTestProject.Utils.Enum;

    /// <summary>
    /// Search Sort Options
    /// </summary>
    public enum SearchSortOptions
    {
        /// <summary>
        /// Relevance Option
        /// </summary>
        [Locator("Relevance")]
        Relevance,

        /// <summary>
        /// Popularity Option
        /// </summary>
        [Locator("Popularity")]
        Popularity,

        /// <summary>
        /// Price Low To High Option
        /// </summary>
        [Locator("Price Low To High")]
        PriceLowToHigh,

        /// <summary>
        /// Price High To Low Option
        /// </summary>
        [Locator("Price High To Low")]
        PriceHighToLow,

        /// <summary>
        /// Discount Option
        /// </summary>
        [Locator("Discount")]
        Discount,

        /// <summary>
        /// Fresh Arrivals Option
        /// </summary>
        [Locator("Fresh Arrivals")]
        FreshArrivals
    }
}
