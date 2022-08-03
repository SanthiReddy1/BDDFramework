Feature: SpecFlowFeature2
	Simple calculator for adding two numbers

@Parallel
Scenario: Testing parallelism
	Given I Open SnapDeal site

@StepArgumentTransformation 
Scenario: Convert timestamp - variant 2 
Given I open timestamp converter
Then I convert 1 day, 2 hours, 3 minutes into timestamp

@StepArgumentTransformation 
Scenario: Convert timestamp - variant 3 
Given I open timestamp converter
Then I convert 1 day, 1 hour, 1 minute, 30 seconds into timestamp

@StepArgumentTransformation
Scenario: Convert into Timestamp 
Given I open timestamp converter
Then I convert 50 days into timestamp