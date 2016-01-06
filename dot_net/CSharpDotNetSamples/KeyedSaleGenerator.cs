using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	// Class for credit card
	public class CreditCard  
	{
		// Declare 'encrypted_number' instead of 'number' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string number { get; set; } 
		public string expiration_month { get; set; }
		public string expiration_year { get; set; }
	}

	// Class for billing address
	public class BillingAddress 
	{
		public string name { get; set; }
		public string street_address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
	}

	// Class for keyed sale request	
	public class KeyedSaleRequest 
	{
		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}
		
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

