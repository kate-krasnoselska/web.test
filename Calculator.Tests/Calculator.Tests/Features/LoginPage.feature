Feature: Login Page
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers
	
@mytag
Scenario: Positive Login
	Given I have entered 'test' into User field
	And I have entered 'newyork1' into Password field
	When I press Login button
	Then I am redirected to Calculator page
