using System;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	public class CaptureTransactionGenerator
	{

		public PayTraceExternalTransResponse CaptureTransactionTrans(string token, CaptureTransactionRequest captureTransactionRequest)
		{
			/// <summary>
			/// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
			/// and Return the object.
			/// Returns the KeyedSaleResponse Type 
			/// </summary>

			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlCapture ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(captureTransactionRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var payTraceExternalTransResponse = new PayTraceExternalTransResponse();
			payTraceExternalTransResponse = JsonSerializer.DeserializeResponse<PayTraceExternalTransResponse>(tempResponse);

			//Assign the http error if any
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)payTraceExternalTransResponse);

			//Return the Desearlized object
			return payTraceExternalTransResponse;
		}


		/*public PayTraceExternalTransResponse CaptureTransactionTrans(string token, CaptureTransactionRequest captureTransactionRequest)
		{

			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlCapture ;

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(captureTransactionRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected PayTraceExternalTransResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data

			PayTraceExternalTransResponse ObjPayTraceExternalTransResponse= new PayTraceExternalTransResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//optional - Display the Json Response
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				ObjPayTraceExternalTransResponse = jsSerializer.Deserialize<PayTraceExternalTransResponse>(TempResponse.JsonResponse);
			}
			ObjPayTraceExternalTransResponse.ErrorMsg = TempResponse.ErrorMessage;
			return ObjPayTraceExternalTransResponse;
		}*/
	}
}

