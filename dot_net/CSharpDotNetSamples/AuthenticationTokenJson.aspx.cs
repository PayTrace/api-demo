using System;
using System.Web;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Text;


namespace CSharpDotNetJsonSample
{
	
	/// <summary>
	/// Authentication token test demo.  
	/// </summary>
	public partial class AuthenticationTokenTest : System.Web.UI.Page
	{
		public void BtnAuthClicked (object sender, EventArgs args)
		{
			if(this.IsPostBack)
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				var result = tokenGenerator.GetToken ();
				DisplayResults(result);
			}
		}

		protected void DisplayResults(OAuthToken result) 
		{
			
			if(result.ErrorFlag == false)
			{
                //Display the result - optional

                Response.Write("OAuth Token Request: " + "Success!" + "<br>");
                Response.Write ("access_token  : " + result.AccessToken + "<br>");    		
				Response.Write ("token_type  : " + result.TokenType  + "<br>");    		
				Response.Write ("expires_in  : " + result.ExpiresIn + "<br>");    		
							
			} 
			else
			{
                // Do you code here to handle the error for the OAuth token

                // display the error - optional
                Response.Write (" OAuth Token Request: " + "Failed!" + "<br>");
                Response.Write (" Http Status Code & Description : " +  result.ObjError.HttpTokenError  + "<br>");
				Response.Write (" API Error : " +  result.ObjError.Error + "<br>");
				Response.Write (" API Error Message : " +  result.ObjError.ErrorDescription+ "<br>");
			}
		}


	}

}


