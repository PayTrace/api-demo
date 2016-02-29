using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;


namespace AspNetClientEncryptionExample
{
	public class VaultSaleByCustomerIDGenerator
	{
		public PayTraceBasicSaleResponse VaultSaleByCustomerIdTrans(string token, VaultSaleByCustomerIdRequest vaultSaleByCustomerIdRequest)
		{

			// Header details are available at Authentication header page.
			string methodUrl = "/v1/transactions/sale/by_customer";

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(vaultSaleByCustomerIdRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var TempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected PayTraceBasicSaleResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data

			PayTraceBasicSaleResponse payTraceBasicSaleResponse = new PayTraceBasicSaleResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//optional - Display the Json Response
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				payTraceBasicSaleResponse = jsSerializer.Deserialize<PayTraceBasicSaleResponse>(TempResponse.JsonResponse);
			}
			payTraceBasicSaleResponse.ErrorMsg = TempResponse.ErrorMessage;

			return payTraceBasicSaleResponse;
		}
	}

}

