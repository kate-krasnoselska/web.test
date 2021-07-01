﻿Feature: Login Page
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

Scenario: Negative Login with empty Password field
	Given I have entered 'test' into User field
		And I have entered '' into Password field
	When I click Login button
	Then I see error message 'User name and password cannot be empty!'

Scenario: Negative Login with empty User name and empty Password field
	Given I have entered '' into User field
		And I have entered '' into Password field
	When I click Login button
	Then I see error message 'User name and password cannot be empty!'

Scenario: Negative Login with Caps Lock Password field
	Given I have entered 'test' into User field
		And I have entered 'NEWYORK1' into Password field
	When I click Login button
	Then I see error message 'Incorrect password!'

Scenario: Negative Login with Caps Lock User field
	Given I have entered 'TEST' into User field
		And I have entered 'newyork1' into Password field
	When I click Login button
	Then I see error message 'Incorrect user name!'

Scenario: Negative Login with incorrect User name
	Given I have entered 'test1' into User field
		And I have entered 'newyork1' into Password field
	When I click Login button
	Then I see error message 'Incorrect user name!'

Scenario: Negative Login with incorrect Password
	Given I have entered 'test' into User field
		And I have entered 'newyork11' into Password field
	When I click Login button
	Then I see error message 'Incorrect password!'

Scenario: Negative Login with space in User name
	Given I have entered ' test' into User field
		And I have entered 'newyork1' into Password field
	When I click Login button
	Then I see error message 'Incorrect user name!'

Scenario: Negative Login with incorrect User name and Incorrect Password
	Given I have entered 'test1' into User field
		And I have entered 'newy ork1' into Password field
	When I click Login button
	Then I see error message ''test1' user doesn't exist!'









