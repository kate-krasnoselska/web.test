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

Scenario: Negative Login with space in User name
	Given I have entered ' test' into User field
		And I have entered 'newyork1' into Password field
	When I click Login button
	Then I see error message 'Incorrect user name!'

Scenario Outline: Negative Login
	Given I have entered '<login>' into User field
		And I have entered '<password>' into Password field
	When I click Login button
	Then I see error message '<error_message_text>'

Examples:
	| login | password | error_message_text |
	|       | newyork1 | User name and password cannot be empty! |
	| test  |          | User name and password cannot be empty! |
	|       |          | User name and password cannot be empty! |
	| TEST  | newyork1 | Incorrect user name!                    |
	| test  | NEWYORK1 | Incorrect password!                     |
	| test1 | newyork1 | Incorrect user name!                    |
	| test  | newyork11| Incorrect password!                     |
	| test  | newyork1 | Incorrect user name!                    |
	| test1 | newyork11| 'test1' user doesn't exist!             |