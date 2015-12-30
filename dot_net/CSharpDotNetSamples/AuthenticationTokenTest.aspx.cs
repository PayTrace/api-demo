using System;
using System.Web;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Text;


namespace AspNetClientEncryptionExample
{
	
	public partial class AuthenticationTokenTest : System.Web.UI.Page
	{
		public void BtnAuthClicked (object sender, EventArgs args)
		{
			if(this.IsPostBack)
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				WriteResults(tokenGenerator.GetToken ());
			}
		}

		protected void WriteResults(OAuthToken result) 
		{
			
			if(result.errorflag == false)
			{
				//Display the result - optional
				Response.Write ("access_token  : " + result.access_token + "<br>");    		
				Response.Write ("token_type  : " + result.token_type + "<br>");    		
				Response.Write ("expires_in  : " + result.expires_in + "<br>");    		
				Response.Write ("Token Request: " + "Success!" + "<br>");			
			} 
			else
			{
				// Do you code here to handle the error for the OAuth token

				// display the error - optional
				Response.Write (" Http Status Code & Description : " +  result.Error.token_error_http  + "<br>");
				Response.Write (" API Error : " +  result.Error.error + "<br>");
				Response.Write (" API Error Message : " +  result.Error.error_description+ "<br>");
				Response.Write (" Token Request: " + "Failed!" + "<br>");
			}
		}


	}

}


