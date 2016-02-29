using System;
//using System.Runtime.Serialization.json ; 
using System.Web.Script.Serialization;
using System.IO;

namespace AspNetClientEncryptionExample
{
	public class TestJsonGenerator
	{
		public PayTraceBasicSaleResponse TestVaultSaleByCustomerIdTrans(string token, VaultSaleByCustomerIdRequest vaultSaleByCustomerIdRequest)
		{
			// Set the URL for the Method
			string methodUrl = ApiEndPointConfiguration.UrlVaultSaleByCustomerId ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(vaultSaleByCustomerIdRequest);

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			PayTraceBasicSaleResponse payTraceBasicSaleResponse = new PayTraceBasicSaleResponse();
			payTraceBasicSaleResponse = JsonSerializer.DeserializeResponse<PayTraceBasicSaleResponse>(tempResponse);
		
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)payTraceBasicSaleResponse);

			return payTraceBasicSaleResponse;		
		}

	

	}
}

