# StatsPerformChallenge
Mock up a simple version of outlined front end requirements for the user to test your new API:
	- To fully test the new API, all endpoints need to be tested, i.e.:
		- GET /api/Brandings (gets a list of available Brandings)
		- GET /api/Leagues (gets a list of available Leagues)
		- GET /api/Leagues/{leagueId}/unbranded-matches (gets all matches of a given league, with no branding applied. Uses "Id" from GET /api/Leagues.)
		- GET /api/Leagues/{leagueId}/branded-matches/{brandingId} (gets all matches of a given league, with selected branding applied Uses "Id" from GET /api/Leagues and "Id" from GET /api/Brands)
	- To list out the requirements specifically:
		- Frontend must populate the League dropdown with values from GET /api/Leagues
		- Frontend must populate the Brandings dropdown with values from GET /api/Brandings
		- Frontend must allow the user to select a league of his choosing
			- After a league is selected, it's ID is used to display the matches to the user (e.g. dropdown + list view/table/grid) (GET /api/Leagues/{leagueId}/unbranded-matches)
		- Frontend must allow the user to select a branding of his choosing (e.g. dropdown) 
			- After a branding is selected, the match list is redrawn using new values (GET /api/Leagues/{leagueId}/branded-matches/{brandingId} )
		- Frontend must allow the user to return back to the unbranded display of matches (e.g. checkbox or extra value in dropdown with additional logic)
