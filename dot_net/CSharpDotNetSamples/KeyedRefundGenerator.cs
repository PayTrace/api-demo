using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;


namespace AspNetClientEncryptionExample
{
	
	public class KeyedRefundGenerator
	{
        /// <summary>
        /// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
		/// and Return the object.
		/// Returns the KeyedRefundResponse Type 
		/// </summary>
		public KeyedRefundResponse KeyedRefundTrans(string token, KeyedRefundRequest keyedRefundRequest)
		{
		
			// Header details are available at ApiEndPointConfiguration
			string methodUrl = ApiEndPointConfiguration.UrlKeyedRefund ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(keyedRefundRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var keyedRefundResponse= new KeyedRefundResponse();
			keyedRefundResponse = JsonSerializer.DeserializeResponse<KeyedRefundResponse>(tempResponse);

			//Assign the http error if any 
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)keyedRefundResponse);

			//Return the Desearlized object
			return keyedRefundResponse;
		}

	}

}

