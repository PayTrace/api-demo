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
		public PayTraceBasicResponse VoidTransactionTrans(string token, VoidTransactionRequest voidTranasactionRequest)
		{

			// Header details are available at Authentication header page.
			string methodUrl =ApiEndPointConfiguration.UrlVoidTransaction ;

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(voidTranasactionRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var TempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected PayTraceBasicResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data

			PayTraceBasicResponse payTraceBasicResponse = new PayTraceBasicResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//optional - Display the Json Response
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				payTraceBasicResponse  = jsSerializer.Deserialize<PayTraceBasicResponse>(TempResponse.JsonResponse);
			}
			payTraceBasicResponse .ErrorMsg = TempResponse.ErrorMessage;
			return payTraceBasicResponse ;
		}
	}
}

