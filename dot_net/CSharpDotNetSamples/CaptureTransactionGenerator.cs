using System;
using System.Collections.Generic ;

namespace CSharpDotNetJsonSample
{
	public class CaptureTransactionGenerator
	{
        /// <summary>
        /// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
        /// and Return the object.
        /// Returns the PayTraceExternalTransResponse Type 
        /// </summary>
        public PayTraceExternalTransResponse CaptureTransactionTrans(string token, CaptureTransactionRequest captureTransactionRequest)
		{

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

	}
}

