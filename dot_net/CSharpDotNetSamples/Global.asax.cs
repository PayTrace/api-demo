using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace AspNetClientEncryptionExample
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start (Object sender, EventArgs e)
		{

		}

		protected void Session_Start (Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest (Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest (Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest (Object sender, EventArgs e)
		{

		}

		protected void Application_Error (Object sender, EventArgs e)
		{
			// Get the exception object.
			Exception exc = Server.GetLastError();
	

			// For other kinds of errors give the user some information
			// but stay on the default page
			Response.Write("<h2>Global Page Error</h2>\n");
			Response.Write(
				"<p>" + exc.Message + "</p>\n");
			Response.Write("Return to the <a href='Default.aspx'>" +
				"Default Page</a>\n");

			// Clear the error from the server
			Server.ClearError();

		}

		protected void Session_End (Object sender, EventArgs e)
		{

		}

		protected void Application_End (Object sender, EventArgs e)
		{

		}
	}
}
