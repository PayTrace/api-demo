using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;


namespace AspNetClientEncryptionExample
{

	/// <summary>
	/// this class holds properties for the KeyedRefund request.
	/// Please check the Account security settings before defining this class as there are some request fields are conditional and optional.
	/// this class uses Billing Address class 
	/// this class also uses Credit Card class
	/// </summary>
	public class KeyedRefundRequest 
	{
		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}


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

