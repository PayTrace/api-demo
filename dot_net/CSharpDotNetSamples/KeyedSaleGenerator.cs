using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	// Class for credit card
	public class CreditCard  
	{
		// Declare 'encrypted_number' instead of 'number' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string number { get; set; } 
		public string expiration_month { get; set; }
		public string expiration_year { get; set; }
	}

	// Class for billing address
	public class BillingAddress 
	{
		public string name { get; set; }
		public string street_address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
	}

	// Class for keyed sale request	
	public class KeyedSaleRequest 
	{
		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}
		
	//TODO: Provide a comment about the class for processing Transactions Response. 
	//For simpilicity only one class has been created to represent the response from multiple credit card transaction
	public class TransactionResponse
	{
		/// <summary>
		/// Available on all transaction with the API
		/// </summary>
		public bool success { get; set; }

		/// <summary>
		/// Available on following transactions
		/// 1. Keyed Sale Response
		/// 2. 
		/// </summary>
		public int response_code { get; set; }

		/// <summary>
		/// Gets or sets the status message.
		/// </summary>
		/// <value>The status message.</value>
		public string status_message { get; set; }
		public int transaction_id { get; set; }
		public string approval_code { get; set; }
		public string approval_message { get; set; }
		public string avs_response { get; set; }
		public string csc_response { get; set; }
		public string external_transaction_id { get; set; }
		public string masked_card_number { get; set; }
		// Optional : To hold http error or any unexpected error, this is not a part of PayTrace API Error Response.
		public string ErrorMsg { get; set; } 

		// to store the error Key with PayTrace API Response
		public Dictionary<string,string[]> errors { get; set; } 
	
	}

	public class KeyedSaleGenerator
	{
		public TransactionResponse KeyedSaleTrans(string token,KeyedSaleRequest keyedSaleRequest)
		{
			// Header details are available at Authentication header page.

			string Baseurl = "https://api.paytrace.com"; //Production.
			//String Baseurl = "https://apitest2.paytrace.com"; // test

			string MethodURL = "/v1/transactions/sale/keyed";

			// variables for request stream and Respone reader 
			Stream dataStream = null;
			StreamReader reader = null;
			WebResponse response = null;

			//converting request into JSON string
			var js = new JavaScriptSerializer();
			var requestJSON = js.Serialize(keyedSaleRequest);

			TransactionResponse resultKeySale = new TransactionResponse();



			try
			{
				//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

				// Create a request using a URL that can receive a post. 
				WebRequest request = WebRequest.Create(Baseurl + MethodURL );

				// Set the Method property of the request to POST.
				request.Method = "POST";

				//to set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
				((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;

				// optional - to set the Accept property, cast the WebRequest object into HttpWebRequest class
				//((HttpWebRequest)request).Accept = "*/*";

				//Set the ContentType property of the WebRequest.
				request.ContentType = "application/json";
				((HttpWebRequest)request).Headers[HttpRequestHeader.Authorization] = token ;

				byte[] byteArray = Encoding.UTF8.GetBytes(requestJSON);

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

				// Assuming Respose status is OK otherwise catch{} will be excuted 
				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream();

				// Open the stream using a StreamReader for easy access.
				reader = new StreamReader(dataStream);

				// Read the content.
				String responseFromServer = reader.ReadToEnd();

				// Display the Response content
				resultKeySale = KeyedSaleResponseData(responseFromServer);

				return resultKeySale ;
			}
			catch(WebException e) 
			{

				// This exception will be raised if the server didn't return 200 - OK within response.
				// Retrieve more information about the error 

				if (e.Response != null)
				{
					using (var responseStream = e.Response.GetResponseStream())
					{
						if (responseStream != null)
						{
							String temp = (new StreamReader(responseStream)).ReadToEnd ();
							resultKeySale = KeyedSaleResponseData (temp); // to parse the json data.
						}
					}

					//Retrive http Error 
					HttpWebResponse err = (HttpWebResponse)e.Response;
					resultKeySale.ErrorMsg = ((int)err.StatusCode) + " " + err.StatusDescription;

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
			return resultKeySale;
		}

		protected TransactionResponse KeyedSaleResponseData(String ResponseData)
		{
			// Create an object to parse JSON data
			TransactionResponse ObjKeyedSaleResponse= null;
			JavaScriptSerializer js = new JavaScriptSerializer ();

			if (null != ResponseData) 
			{
				// parse JSON data into C# obj
				//ObjKeyedSaleResponse = JsonConvert.DeserializeObject<ProcessResponse> (ResponseData);
				ObjKeyedSaleResponse = js.Deserialize<TransactionResponse>(ResponseData);
				ObjKeyedSaleResponse.ErrorMsg = null;
			} 
			return ObjKeyedSaleResponse;
		}
			
	
	}


}

