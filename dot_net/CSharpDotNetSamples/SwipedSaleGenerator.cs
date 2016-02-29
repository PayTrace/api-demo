using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;


namespace AspNetClientEncryptionExample
{

	// class for all Swiped Sale Request process and JSON response
	public class SwipedSaleGenerator
	{
		public PayTraceBasicSaleResponse SwipedSaleTrans(string token, SwipedSaleRequest swipedSaleRequest)
		{
			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlSwipedSale ;

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(swipedSaleRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var TempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);

		}

		public PayTraceBasicSaleResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create objects to parse JSON data
			PayTraceBasicSaleResponse payTraceBasicSaleResponse = new PayTraceBasicSaleResponse();
			var jsSerializer = new JavaScriptSerializer ();

			//Optional - Display Json Response before parsing into Object.
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

