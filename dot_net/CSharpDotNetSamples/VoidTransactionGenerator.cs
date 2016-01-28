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
		public PayTraceBasicResponse VoidTransactionTrans(string token, VoidTransactionRequest voidTrnasactionRequest)
		{

			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/void";

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(voidTrnasactionRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected PayTraceBasicResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data

			PayTraceBasicResponse ObjPayTraceBasicResponse= new PayTraceBasicResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//optional - Display the Json Response
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				ObjPayTraceBasicResponse = jsSerializer.Deserialize<PayTraceBasicResponse>(TempResponse.JsonResponse);
			}
			ObjPayTraceBasicResponse.ErrorMsg = TempResponse.ErrorMessage;
			return ObjPayTraceBasicResponse;
		}
	}
}

