using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	
	public class KeyedSaleGenerator
	{

		public KeyedSaleResponse KeyedSaleTrans(string token, KeyedSaleRequest keyedSaleRequest)
		{
			/// <summary>
			/// Method for builiding json request before making actual web request
			/// Returns the KeyedSaleResponse Type 
			/// </summary>

			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/sale/keyed";

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string

			var requestJSON = jsSerializer.Serialize(keyedSaleRequest);

			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Return the Desearlized object
			return DeserializeResponse(TempResponse);
		}

		public KeyedSaleResponse KeyedAuthorizationTrans(string token, KeyedSaleRequest keyedSaleRequest)
		{
			/// <summary>
			/// Method for builiding json request before making actual web request
			/// Returns the KeyedSaleResponse Type as it is exactly same as Keyed Authorization.
			/// </summary>


			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/authorization/keyed";

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string

			var requestJSON = jsSerializer.Serialize(keyedSaleRequest);

			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Return the Desearlized object
			return DeserializeResponse(TempResponse);
		}


		protected KeyedSaleResponse DeserializeResponse(TempResponse TempResponse)
		{

			// Create an object to parse JSON data
			KeyedSaleResponse ObjKeyedSaleResponse= new KeyedSaleResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//Optional - Display Json Response before parsing into Object.
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				ObjKeyedSaleResponse = jsSerializer.Deserialize<KeyedSaleResponse>(TempResponse.JsonResponse);
			}
			ObjKeyedSaleResponse.ErrorMsg = TempResponse.ErrorMessage;
			return ObjKeyedSaleResponse;
		}


			
	}

}

