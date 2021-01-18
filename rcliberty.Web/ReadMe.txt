//////////////////////////////////////////////////////////////////////////////////
//						
//						General Guidelines
//						
//////////////////////////////////////////////////////////////////////////////////

1. Deployment
==================================================================================

After successfully making your changes and building your solution/project(s). They
are ready to be deployed as follows:
	1. Connect to the domain via FTP

2. Deployment
==================================================================================
The following is a listing of the required folders and files needed to render a
MVC web application using .NET Framework 4.5.2
	* bin (dir): code is compiled down so the browser can understand
	* Content (dir): images, css, etc. can be stored here
	* Scripts (dir): JavaScript files
	* Views (dir): all of the views for all controllers; this includes the VIEWS web.config
	* fonts (dir): if applicable (may be in with Content)
	* any external .cs files that are needed to perform necessary functions (email settings, etc)
	* favicon.ico: if applicable
	* Global.asax: determines how to handle server and session events
	* pacakges.config: a listning of all packages installed and their location (relative to bin)
	* Web.config (root): the root Web.config file to help render the site appropriately
//////////////////////////////////////////////////////////////////////////////////
//						
//						Identity User Role Management
//						
//////////////////////////////////////////////////////////////////////////////////


1. Introduction
==================================================================================

Thank you for installing Identity User Role Management. This NuGet package 
provides UI based User and Role Management for ASP.NET MVC applications using 
ASP.NET Identity.


2. Initilization Instructions
==================================================================================
	1. Install the NuGet package 
	2. Configure the DefaultConnection of the web.config file to point to the 
	   desired database
	3. Run the application and call the InitAdmin controller
	   EX: localhost:1234/InitAdmin
	4. Enter the desired email and password for the administrator and submit

*********************************************************************************
*********************************************************************************
*****																		*****
*****								WARNING									*****
*****																		*****
*********************************************************************************
*********************************************************************************

It is highly recommended that you delete or lock down the InitAdmin controller 
and views before deployment due to security issues. The InitAdmin functions
can be used to create additional Admin accounts even after initialization. 


3. Usage Instructions
==================================================================================

	1. To manage users call the UsersAdmin index view (domain.com/UsersAdmin)

	2. To manage roles call the RolesAdmin index view (domain.com/RolesAdmin)

	3. Add the following code to your main navigation menu to easily provide secured
	   access to the UsersAdmin and RolesAdmin

		@if (Request.IsAuthenticated && User.IsInRole("Admin"))
		{
				<a href="@Url.Action("Index","UsersAdmin")">Users Admin</a>
				<a href="@Url.Action("Index","RolesAdmin")">Rules Admin</a>
		}

==================================================================================

Happy Coding!

Jason Presley
http://www.jasonpresley.me