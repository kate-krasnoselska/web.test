Feature: Login Page
	The User can log in to program 
	Using correct credentials
	And can not login using incorrect credentials
	
@mytag
Scenario: Positive Login
	Given I have entered 'test' into User field
		And I have entered 'newyork1' into Password field
	When I click Login button
	Then I am redirected to Calculator page

Scenario: Negative Login with empty User name
	Given I have entered '' into User field
		And I have entered 'newyork1' into Password field
	When I click Login button
	Then I see error message 'User name and password cannot be empty!'