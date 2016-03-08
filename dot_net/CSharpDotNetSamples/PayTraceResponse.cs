using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using Newtonsoft.Json;


namespace CSharpDotNetJsonSample
{	
	/// <summary>
	/// class to hold temprory Json Response and Error message for any response.
	/// </summary>
	public class TempResponse
	{
		public string JsonResponse { get; set; }
		public string ErrorMessage { get; set; }
	}

	/// <summary>
	/// class for the holding  oAuth Data 
	/// </summary>
	public class OAuthToken
	{
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

		// for Errors
		// Object for PayTrace Error Json Key
		public OAuthError ObjError { get; set; }

		// Optional - flag for error 
		public Boolean ErrorFlag { get; set;}

	}

	/// <summary>
	/// Class that holds Error for the OAuth token
	/// </summary>
	public class OAuthError
	{
        // Json key - returned by PayTrace API for error
        [JsonProperty("error")]
        public string Error { get; set; }

        // Json key - returned by PayTrace API for error
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

		// optional - for http error : 
        // this proeprty is userdefined and it has been used to store a http error 
		public string HttpTokenError{ get; set;}  
	}

	/// <summary>
	/// /// Properties Available on most of the transaction responses with the API
	/// </summary>
	public class PayTraceBasicResponse
	{

        [JsonProperty("success")]
		public bool Success { get; set; }

        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        /// <summary>
        /// transaction_id is not a part of Error Response , Create Customer Profile Requests 
        /// </summary>
        [JsonProperty("transaction_id")]
        public long TransactionId { get; set; }

		// Optional : To hold http error or any unexpected error, this is not a part of PayTrace API Error Response.
		public string HttpErrorMessage { get; set; }

        // to store the error Key with PayTrace API Response
        [JsonProperty("errors")]
        public Dictionary<string,string[]> TransactionErrors { get; set; } 

	}

	/// <summary>
	/// Following properties are Available on some of the Responses with the API
	/// This class is used by some of the child classes on this page 
	/// As well as for the Capture Transaction 
	/// </summary>
	public class PayTraceExternalTransResponse : PayTraceBasicResponse
	{
        [JsonProperty("external_transaction_id")]
        public string ExternalTransactionId { get; set; }
	}


	/// <summary>
	/// PayTraceBasicSaleResponse Class 
    /// following properties are Available on most of the Sale Responses with the API
	/// </summary>
	public class PayTraceBasicSaleResponse : PayTraceExternalTransResponse
	{

        [JsonProperty("approval_code")]
        public string ApprovalCode { get; set; }

        [JsonProperty("approval_message")]
        public string ApprovalMessage { get; set; }

        [JsonProperty("avs_response")]
        public string AvsResponse { get; set; }

        [JsonProperty("csc_response")]
        public string CscResponse { get; set; }

	}

    /// <summary>
    /// KeyedSaleResponse Class 
    /// following properties are Specific to Keyed Sale Response and Keyed Authorization Response
    /// </summary>
    public class KeyedSaleResponse : PayTraceBasicSaleResponse
	{
        [JsonProperty("masked_card_number")]
        public string MaskedCardNumber { get; set; }
	}


    /// <summary>
    /// KeyedRefundResponse Class 
    /// following properties are Specific to Keyed Refund Response
    /// you could given generic name to above class KeyedSaleResposne and use it for the Keyed Refund  
    /// To avoid any confusion - Created seperate class for most of the transaction
    /// </summary>
    public class KeyedRefundResponse: PayTraceBasicSaleResponse
	{
        [JsonProperty("masked_card_number")]
        public string MaskedCardNumber { get; set; }
	}


	/// <summary>
	/// following properties are Specific to the Create Customer Profile response.
	/// Declare any other properties based on the Security checking page settings and DisCreationary Data Tab
	/// </summary>
	public class CreateCustomerProfileResponse : PayTraceBasicResponse
	{
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("masked_card_number")]
        public string MaskedCardNumber { get; set; }

	}

	public class PayTraceResponse
	{

		public TempResponse ProcessTransaction(string methodUrl,string token, string requestData)
		{

			// Header details are available at Authentication header page.

			string Baseurl = ApiEndPointConfiguration.BaseUrl; //Production.

			// variables for request stream and Respone reader 
			Stream dataStream = null;
			StreamReader reader = null;
			WebResponse response = null;

			TempResponse objTempResponse = new TempResponse ();

			try
			{
				//Set the request header

				//Create a request using a URL that can receive a post. 
				WebRequest request = WebRequest.Create(Baseurl + methodUrl);

				// Set the Method property of the request to POST.
				request.Method = "POST";

				//to set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
				((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;

				// optional - to set the Accept property, cast the WebRequest object into HttpWebRequest class
				//((HttpWebRequest)request).Accept = "*/*";

				//Set the ContentType property of the WebRequest.
				request.ContentType = "application/json";
				//set the Authorization token
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

				// Assign/store Transaction Json Response to TempResposne Object 
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



