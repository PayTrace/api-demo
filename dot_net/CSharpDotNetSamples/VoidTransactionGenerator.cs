using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	public class VoidTransactionGenerator
	{
        /// <summary>
        /// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
        /// and Return the object.
        /// Returns the PayTraceBasicResponse Type 
        /// </summary>
        public PayTraceBasicResponse VoidTransactionTrans(string token, VoidTransactionRequest voidTranasactionRequest)
		{

            // Header details are available at Authentication header page.
            string methodUrl =ApiEndPointConfiguration.UrlVoidTransaction ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(voidTranasactionRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var payTraceBasicResponse = new PayTraceBasicResponse();
			payTraceBasicResponse = JsonSerializer.DeserializeResponse<PayTraceBasicResponse>(tempResponse);

			//Assign the http error if any
			JsonSerializer.AssignError(tempResponse,payTraceBasicResponse);

			//Return the Desearlized object
			return payTraceBasicResponse;
		}

	}
}

