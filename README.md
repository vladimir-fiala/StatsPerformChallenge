# StatsPerformChallenge<br>
<h3>Project overview</h3>
This project was implemented using Visual Studio 2022.
It is an ASP.NET Core Web API with several endpoints and XUnit testing project.
<hr>
<h3>Step-by-step execution</h3>
<ol>
<li>Install Visual Studio 2022 and include "ASP.NET and web development"</li>
<li>Clone repository or download source files</li>
<li>Open the solution file (StatsPerformChallenge.sln)</li>
<li>Prepare the SAS Token (see below)</li>
<li>Make sure the startup project is set to "StatsPerformChallengeAPI"</li>
<li>Run the project by via the "Debug" menu > "Debug Debugging" or hitting F5</li>
<li>Your browser should open and the Swagger page should be displayed, where you can interact with the API</li>
</ol>
<h3>SAS Token preparation:</h3>
As the SAS Token is confidential, you must first set it to your local project.<br>
Use the following steps to do so:<br>
<ol>
<li>Open your favorite command line tool and navigate to the solution folder (e.g. C:\Users\vfiala\Downloads\StatsPerformChallenge-master)</li>
<li>Navigate to the API project directory (StatsPerformChallengeAPI)</li>
<li>Run the following command, replacing {token-string} with the actual SAS Token: <code>dotnet user-secrets set "protectedFiles:SasToken" "{token-string}"</code></li>
<li>NOTE: If your user-secrets has not yet been initialized, run the following command: <code>dotnet user-secrets init</code></li>
<li>You should get a confirmation that the SasToken was successfully set. You can use the following command to verify: <code>dotnet user-secrets list</code></li>
</ol>
If you do not set the SAS Token, the API will return Status Code 500 with message "Values could not be read", and the application console will report GET {endpoint-address} encountered an issue NotFound (Not Found)<br>
<hr>
<h3>SAS Token workaround:</h3>
Alternatively, you can hardcode the token by editing the Extension method <code>public static string AppendSasToken(this string str)</code> defined in Extensions/ExtensionMethods<br>
<br>


<hr>
<h3>Extra tasks</h3>
Mock up a simple version of outlined front end requirements for the user to test your new API<br>
To fully test the new API, all endpoints need to be tested, i.e.:<br>
<ol>
<li>GET /api/Brandings <br>gets a list of available Brandings</li>
<li>GET /api/Leagues <br>gets a list of available Leagues</li>
<li>GET /api/Leagues/{leagueId}/unbranded-matches<br>gets all matches of a given league, with no branding applied.<br>Uses "Id" from GET /api/Leagues.</li>
<li>GET /api/Leagues/{leagueId}/branded-matches/{brandingId} <br>gets all matches of a given league, with selected branding applied <br>Uses "Id" from GET /api/Leagues and "Id" from GET /api/Brandings)<br></li>
</ol>
To list out the requirements specifically:<br>
- Frontend must populate the League dropdown with values from GET /api/Leagues<br>
- Frontend must populate the Brandings dropdown with values from GET /api/Brandings<br>
- Frontend must allow the user to select a league of his choosing<br>! After a league is selected, it's ID is used to display the matches to the user (e.g. dropdown + list view/table/grid) (GET /api/Leagues/{leagueId}/unbranded-matches)<br>
- Frontend must allow the user to select a branding of his choosing (e.g. dropdown) <br>! After a branding is selected, the match list is redrawn using new values (GET /api/Leagues/{leagueId}/branded-matches/{brandingId} )<br>
- Frontend must allow the user to return back to the unbranded display of matches (e.g. checkbox or extra value in dropdown with additional logic)<br>
