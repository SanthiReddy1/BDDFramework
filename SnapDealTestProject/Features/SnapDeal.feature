@SnapDeal @Search
Feature: SnapDeal
	
@Search @Cart
Scenario: Verify sort by filter options
	Given I Open SnapDeal site
	Then I verify search bar is displayed
	When I search for 'Perfumes'
	Then I verify menu icon displayed
	When I apply 'Price Low To High' SortBy filter
	Then I verify 'Price Low To High' SortBy filter is applied

@HomePageIcons
Scenario: Verify SnapDeal home page has all neceesary icons
	Given I Open SnapDeal site
	Then I verify the icons displayed on the home page
