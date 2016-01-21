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
			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/refund/keyed";
							
			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(keyedRefundRequest);

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

			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				ObjKeyedRefundResponse = jsSerializer.Deserialize<KeyedRefundResponse>(TempResponse.JsonResponse);
			}
			ObjKeyedRefundResponse.ErrorMsg = TempResponse.ErrorMessage;
			return ObjKeyedRefundResponse;
		}
	}

}

