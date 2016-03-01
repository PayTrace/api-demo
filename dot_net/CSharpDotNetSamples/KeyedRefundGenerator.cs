using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;


namespace AspNetClientEncryptionExample
{
	
	public class KeyedRefundGenerator
	{

		public KeyedRefundResponse KeyedRefundTrans(string token, KeyedRefundRequest keyedRefundRequest)
		{
			/// <summary>
			/// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
			/// and Return the object.
			/// Returns the KeyedSaleResponse Type 
			/// </summary>

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


		/*public KeyedRefundResponse KeyedRefundTrans(string token, KeyedRefundRequest keyedRefundRequest)
		{
			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlKeyedRefund ;
							
			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(keyedRefundRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected KeyedRefundResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data

			KeyedRefundResponse ObjKeyedRefundResponse= new KeyedRefundResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//Optional - Display the Json Response 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				ObjKeyedRefundResponse = jsSerializer.Deserialize<KeyedRefundResponse>(TempResponse.JsonResponse);
			}
			ObjKeyedRefundResponse.ErrorMsg = TempResponse.ErrorMessage;
			return ObjKeyedRefundResponse;
		}*/


	}

}

