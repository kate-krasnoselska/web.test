Scenario: Login with correct login and incorrect password
	Given I navigate to Login Page
	When I enter 'test1' to login field
		And I enter 'newyork11' to password field
		And click the Login button
	Then I see the shown error ''test1' user doesn't exist!'

