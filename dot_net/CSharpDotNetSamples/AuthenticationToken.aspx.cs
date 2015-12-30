using System;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Text;
using System.IO;
//using Newtonsoft.Json; //  3rd party package should be added to parse JSON data 
using System.Web.Script.Serialization ; // to parse the Json Data.



namespace AspNetClientEncryptionExample
{
	
	public partial class AuthenticationToken : System.Web.UI.Page
	{

		public void BtnAuthClicked (object sender, EventArgs args)
		{
			if(this.IsPostBack)
			{
				WriteResults(GetAutheticationToken());
			}
		}
		// class for the holding oAuth Data 

		public class OauthToken
		{
			public string access_token { get; set; }
			public string token_type { get; set; }
			public int expires_in { get; set; }
		}

		protected Boolean GetAutheticationToken() 
		{
			Boolean result;
	
			// Those URL are available at Authentication 
			String Baseurl = "http://api.paytrace.com";
			String AuthURL = "/oauth/token";

			// variables for request stream and Respone reader 
			Stream dataStream = null;
			StreamReader reader = null;
			WebResponse response = null;

			try
			{
				
				// Create a request using a URL that can receive a post. 
				WebRequest request = WebRequest.Create(Baseurl + AuthURL );

				// Set the Method property of the request to POST.
				request.Method = "POST";
			
				//to set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
				((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;

				// optional - to set the Accept property, cast the WebRequest object into HttpWebRequest class
				((HttpWebRequest)request).Accept = "*/*";

				//Set the ContentType property of the WebRequest.
				request.ContentType = "application/x-www-form-urlencoded";

				// Create Request data and convert it to a byte array.
				//String requestData = "grant_type=password&username=emo123&password=demo123";
				String requestData = "grant_type=password&username=RupaApi&password=Rup@D3m0";
				byte[] byteArray = Encoding.UTF8.GetBytes(requestData);

				// Set the ContentLength property of the WebRequest.
				request.ContentLength = byteArray.Length ;

				// Get the request stream.
				dataStream = request.GetRequestStream ();

				// Write the data to the request stream.
				dataStream.Write (byteArray, 0, byteArray.Length);
				// Close the Stream object.
				dataStream.Close ();

				// To Get the response.
				response = request.GetResponse ();

				// Display the status - assuming OK status.
				Response.Write ("Response Status: " + ((HttpWebResponse)response).StatusDescription  + "<br>" );

				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream ();

				// Open the stream using a StreamReader for easy access.
				reader = new StreamReader (dataStream);

				// Read the content.
				String responseFromServer = reader.ReadToEnd ();

				// Display the Response content
				result = WriteResponseData(responseFromServer);
			
			}
			catch(Exception e) 
			{

				// This exception will be raised if the server didn't return 200 - OK within response.
				// Retrieve more information about the error
				Response.Write ("Error : " + e.Message + "<br>" );
				result = false;
			
			}
		
			finally 
			{
				// Clean up the streams.
				if (null != reader) 
					reader.Close ();

				if (null != dataStream) 
					dataStream.Close ();
				
				if (null != response) 
					response.Close ();
			
			}
			return result;
		
		}

		protected Boolean WriteResponseData(String ResponseData)
		{
			// Create an object to parse JSON data
			OauthToken ObjOauthToken = new OauthToken ();
			JavaScriptSerializer js = new JavaScriptSerializer ();
			if (null != ResponseData) 
			{
				Response.Write (" JSON Response Data :" + ResponseData + "<BR>");

				// parase JSON data into C# obj
				//ObjOauthToken = JsonConvert.DeserializeObject<OauthToken> (ResponseData);
				ObjOauthToken = js.Deserialize<OauthToken>(ResponseData);

				
				//display the response.
				Response.Write (" JSON Response Data parse into C# :" + "<BR>");
				Response.Write ("access_token:" + ObjOauthToken.access_token + "<BR>");
				Response.Write ("token_Type:" + ObjOauthToken.token_type + "<BR>");				
				Response.Write ("Expires_in :" + ObjOauthToken.expires_in + "<BR>");
			
				return true;
			}
			else
				return false;
		}

		protected void WriteResults(Boolean result) 
		{
			if (result == true) 
				Response.Write ("OAuth Authentication : " + "Success!" + "<br>");
			 
			else 
				Response.Write ("OAuthAuthentication  : " + "Failed!" + "<br>");    		
		}

	}
}

