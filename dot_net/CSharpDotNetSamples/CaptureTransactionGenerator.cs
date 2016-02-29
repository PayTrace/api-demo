using System;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	public class CaptureTransactionGenerator
	{
		public PayTraceExternalTransResponse CaptureTransactionTrans(string token, CaptureTransactionRequest captureTransactionRequest)
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
		}
	}
}

