using System;

namespace CSharpDotNetJsonSample
{
	public class KeyedAuthorizationGenerator
	{
		public KeyedSaleResponse KeyedAuthorizationTrans(string token, KeyedSaleRequest keyedSaleRequest)
		{
			/// <summary>
			/// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
			/// and Return the object.
			/// Returns the KeyedSaleResponse Type 
			/// </summary>

			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlKeyedAuthorization;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(keyedSaleRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var keyedSaleResponse = new KeyedSaleResponse();
			keyedSaleResponse = JsonSerializer.DeserializeResponse<KeyedSaleResponse>(tempResponse);

			//Assign the http error if any
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)keyedSaleResponse);

			//Return the Desearlized object
			return keyedSaleResponse;
		}
			
	}
}

