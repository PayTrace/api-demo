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
			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/sale/keyed";

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string

			var requestJSON = jsSerializer.Serialize(keyedSaleRequest);

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected KeyedSaleResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data
			KeyedSaleResponse ObjKeyedSaleResponse= new KeyedSaleResponse();
			var jsSerializer= new JavaScriptSerializer ();

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

