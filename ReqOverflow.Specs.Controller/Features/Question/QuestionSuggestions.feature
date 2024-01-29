Feature: Question suggestions

The user receives a suggested list of related questions while entering a new question
in order to avoid posting a duplicate.

Rule: Questions with at least one common word should be suggested, excluding common English words

Scenario Outline: There is a question with common words of the one being asked
	Given there are questions asked as
		| title             |
		| What is Reqnroll? |
		| What is Cucumber? |
	And the user is authenticated
	When the user starts asking a question as
		| title            |
		| <asked question> |
	Then the suggestions list should be
		| title             |
		| What is Reqnroll? |
Examples: 
	| description                               | asked question                     |
	| Another question contains the same word   | Best Reqnroll practices            |
	| Word match is case insensitive            | Best Reqnroll practices            |
	| Word 'is' is ignored from second question | Is this the best Reqnroll practice |


Rule: Words should be matched in title, body and tags

Scenario: The same word is in different fields
	Given there are questions asked as
		| title             | body               | tags     |
		| What is Reqnroll? | Body 1             |          |
		| Question 2        | I'm using Reqnroll |          |
		| Question 3        | I'm using Reqnroll | Reqnroll |
	And the user is authenticated
	When the user starts asking a question as
		| title                   |
		| Best Reqnroll practices |
	Then the suggestions list should be
		| title             |
		| What is Reqnroll? |
		| Question 2        |
		| Question 3        |


Rule: Suggestions with more common words should be earlier in the list

Scenario: There are existing questions with multiple common words
	Given there are questions asked as
		| title                                 |
		| What is Reqnroll?                     |
		| Who is the best Reqnroll contributor? |
	And the user is authenticated
	When the user starts asking a question as
		| title                   |
		| Best Reqnroll practices |
	Then the suggestions list should be provided in this order
		| title                                 |
		| Who is the best Reqnroll contributor? |
		| What is Reqnroll?                     |


