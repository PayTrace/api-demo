using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web ;



namespace AspNetClientEncryptionExample
{
	public class TempResponse
	{
		/// <summary>
		/// class to hold temprory Json Response and Error message for any response.
		/// </summary>
		public string JsonResponse { get; set; }
		public string ErrorMessage { get; set; }
	}



	public class OAuthToken
	{
		/// <summary>
		/// class for the holding oAuth Data 
		/// </summary>

		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }

		//for Errors
		// Optional - flag for error
		public Boolean errorflag { get; set;}
		// Object for PayTrace Error Json Key
		public OAuthError Error { get; set; }
	}

	public class OAuthError
	{
		/// <summary>
		/// Class that holds Error for the OAuth token
		/// </summary>

		// Json key - returned by PayTrace API for error
		public string error { get; set; } 

		// Json key - returned by PayTrace API for error
		public string error_description { get; set; }

		// optional - for http error : 
		public string token_error_http{ get; set;}  
	}

	public class PayTraceBasicResponse
	{
		/// <summary>
		/// Properties Available on most of the transaction responses with the API
		/// </summary>
		public bool success { get; set; }
	
		public int response_code { get; set; }

		/// <summary>
		/// Gets or sets the status message.
		/// </summary>
		public string status_message { get; set; }

		/// <summary>
		/// transaction_id is not a part of Error Response , Create Customer Profile Requests 
		/// </summary>
		public long transaction_id { get; set; }

		// Optional : To hold http error or any unexpected error, this is not a part of PayTrace API Error Response.
		public string ErrorMsg { get; set; } 

		// to store the error Key with PayTrace API Response
		public Dictionary<string,string[]> errors { get; set; } 

	}
	public class PayTraceBasicResponse1 : PayTraceBasicResponse
	{
		/// <summary>
		///  following properties are Available on most of the Sale Responses with the API
		/// </summary>

		public string external_transaction_id { get; set; }

	}

	public class PayTraceBasicSaleResponse : PayTraceBasicResponse1
	{
		/// <summary>
		///  following properties are Available on most of the Sale Responses with the API
		/// </summary>
		public string approval_code { get; set; }
		public string approval_message { get; set; }
		public string avs_response { get; set; }
		public string csc_response { get; set; }

	}

	public class KeyedSaleResponse : PayTraceBasicSaleResponse
	{
		/// <summary>
		/// following properties are Specific to Keyed Sale Response
		/// </summary>
		public string masked_card_number { get; set; }
	}

	public class KeyedRefundResponse: PayTraceBasicSaleResponse
	{
		/// <summary>
		/// following properties are Specific to Keyed Refund Response
		/// </summary>
		public string masked_card_number { get; set; }
	}

	public class CreateCustomerProfileResponse : PayTraceBasicResponse
	{
		/// <summary>
		/// following properties are Specific to the Create Customer Profile response.
		/// Declare any other properties based on the Security checking page settings and DisCreationary Data Tab
		/// </summary>
		public string customer_id { get; set; }
		public string masked_card_number { get; set; }

	}


	public class PayTraceResponse
	{

		public TempResponse ProcessTransaction(string MethodUrl,string token, string requestData)
		{

			// Header details are available at Authentication header page.

			String Baseurl = "https://api.paytrace.com"; //Production.
			//String Baseurl = "https://apitest2.paytrace.com"; // test

			// variables for request stream and Respone reader 
			Stream dataStream = null;
			StreamReader reader = null;
			WebResponse response = null;

			TempResponse objTempResponse = new TempResponse ();

			try
			{
				//Set the request header
				// Create a request using a URL that can receive a post. 
				WebRequest request = WebRequest.Create(Baseurl + MethodUrl);

				// Set the Method property of the request to POST.
				request.Method = "POST";

				//to set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
				((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;

				// optional - to set the Accept property, cast the WebRequest object into HttpWebRequest class
				//((HttpWebRequest)request).Accept = "*/*";

				//Set the ContentType property of the WebRequest.
				request.ContentType = "application/json";
				((HttpWebRequest)request).Headers[HttpRequestHeader.Authorization] = token ;

				byte[] byteArray = Encoding.UTF8.GetBytes(requestData);

				// Set the ContentLength property of the WebRequest.
				request.ContentLength = byteArray.Length ;

				// Get the request stream.
				dataStream = request.GetRequestStream();

				// Write the data to the request stream.
				dataStream.Write (byteArray, 0, byteArray.Length);
				// Close the Stream object.
				dataStream.Close();

				// To Get the response.
				response = request.GetResponse();

				// Assuming Response status is OK otherwise catch{} will be excuted 
				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream();

				// Open the stream using a StreamReader for easy access.
				reader = new StreamReader(dataStream);

				// Read the content.
				string responseFromServer = reader.ReadToEnd();

				// Convert Json Response into Process Response Object.
				objTempResponse.JsonResponse = responseFromServer;

				return objTempResponse ;
			}
			catch(WebException e) 
			{

				// This exception will be raised if the server didn't return 200 - OK within response.
				// Retrieve more information about the error and API resoponse

				if (e.Response != null)
				{

					//to retrieve the actual JSON response when any error occurs.
					using (var responseStream = e.Response.GetResponseStream())
					{
						if (responseStream != null)
						{
							string temp = (new StreamReader(responseStream)).ReadToEnd();
							objTempResponse.JsonResponse = temp; 
						}
					}

					//Retrive http Error 
					HttpWebResponse err = (HttpWebResponse)e.Response;
					if(err != null)
						objTempResponse.ErrorMessage = ((int)err.StatusCode) + " " + err.StatusDescription;
				}	
				//Do your own error logging in this case
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

			//Do your code here
			return objTempResponse ;

		}

	}



}



