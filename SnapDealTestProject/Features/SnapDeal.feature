@SnapDeal @Search
Feature: SnapDeal
	
@Search @Cart
Scenario Outline: Verify sort by filter options
	Given I Open SnapDeal site
	Then I verify search bar is displayed
	When I search for 'Perfumes'
	Then I verify menu icon displayed
	When I apply '<Filter>' SortBy filter
	Then I verify '<Filter>' SortBy filter is applied
Examples: 
| Filter            |
| Price Low To High |
| Price High To Low |

@HomePageIcons
Scenario: Verify SnapDeal home page has all neceesary icons
	Given I Open SnapDeal site
	Then I verify the icons displayed on the home page

@Excel
@DataSource:../Resources/TestData/SortByFilterData.xlsx
Scenario: Verify sort by filter options using excel data
	Given I Open SnapDeal site
	Then I verify search bar is displayed
	When I search for 'Perfumes'
	Then I verify menu icon displayed
	When I apply '<Filter>' SortBy filter
	Then I verify '<Filter>' SortBy filter is applied