using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;


namespace AspNetClientEncryptionExample
{
	// class for swipedSaleRequest
	public class SwipedSaleRequest
	{
		public double amount { get; set; }
		//declare 'encrypted_swipe' instead of 'swipe' in case of using PayTrace client side encryption
		public string swipe { get; set; }  
	}


	// class for all Swiped Sale Request process and 
	public class SwipedSaleGenerator
	{
		public TransactionResponse SwipedSaleTrans(string token, SwipedSaleRequest swipedSaleRequest)
		{
			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/sale/swiped";

			var jsSerializer = new JavaScriptSerializer();
		
			//converting request into JSON string

			var requestJSON = jsSerializer.Serialize(swipedSaleRequest);

			return ProcessTransaction(methodUrl, token, requestJSON);
		}

		public TransactionResponse DeserializeResponse(String ResponseData)
		{
			// Create objects to parse JSON data
			TransactionResponse ObjResponse= null;
			var jsSerializer = new JavaScriptSerializer ();

			if (null != ResponseData) 
			{
				// parse JSON data into C# obj
				ObjResponse = jsSerializer.Deserialize<TransactionResponse>(ResponseData);
				ObjResponse.ErrorMsg = null;
			} 
			return ObjResponse;
		
		}

		public TransactionResponse ProcessTransaction(string MethodUrl,string token, string requestData)
		{

			// Header details are available at Authentication header page.

			String Baseurl = "https://api.paytrace.com"; //Production.
			//String Baseurl = "https://apitest2.paytrace.com"; // test

			// variables for request stream and Respone reader 
			Stream dataStream = null;
			StreamReader reader = null;
			WebResponse response = null;

			TransactionResponse objTransactionResponse = new TransactionResponse();

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

				// Assuming Respose status is OK otherwise catch{} will be excuted 
				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream();

				// Open the stream using a StreamReader for easy access.
				reader = new StreamReader(dataStream);

				// Read the content.
				String responseFromServer = reader.ReadToEnd();

				// Convert Json Response into Process Response Object.
				objTransactionResponse = DeserializeResponse(responseFromServer);

				return objTransactionResponse  ;
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
							objTransactionResponse  = DeserializeResponse(temp); // to parse the json data.
						}
					}
					//Retrive http Error 
					HttpWebResponse err = (HttpWebResponse)e.Response;
					objTransactionResponse.ErrorMsg = ((int)err.StatusCode) + " " + err.StatusDescription;
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
			return objTransactionResponse ;

		}
	
	}
}

