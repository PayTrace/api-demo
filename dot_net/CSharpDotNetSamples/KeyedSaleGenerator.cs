using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
    /// <summary>
    /// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
    /// and Return the object.
    /// Returns the KeyedSaleResponse Type 
    /// </summary>
    public class KeyedSaleGenerator
	{
        public KeyedSaleResponse KeyedSaleTrans(string token, KeyedSaleRequest keyedSaleRequest)
		{
			
			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlKeyedSale ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(keyedSaleRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var PayTraceResponse = new PayTraceResponse();
			var tempResponse = PayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var keyedSaleResponse = new KeyedSaleResponse();
			keyedSaleResponse = JsonSerializer.DeserializeResponse<KeyedSaleResponse>(tempResponse);

			//Assign the http error 
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)keyedSaleResponse); 

			//Return the Desearlized object
			return keyedSaleResponse;

		}			
	}

}

