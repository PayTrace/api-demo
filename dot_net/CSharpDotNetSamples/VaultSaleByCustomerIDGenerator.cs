using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;


namespace AspNetClientEncryptionExample
{
	public class VaultSaleByCustomerIdGenerator
	{
        /// <summary>
        /// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
        /// and Return the object.
        /// Returns the PayTraceBasicSaleResponse  Type 
        /// </summary>
        public PayTraceBasicSaleResponse VaultSaleByCustomerIdTrans(string token, VaultSaleByCustomerIdRequest vaultSaleByCustomerIdRequest)
		{

            // Header details are available at Authentication header page.
            string methodUrl = ApiEndPointConfiguration.UrlVaultSaleByCustomerId ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(vaultSaleByCustomerIdRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var payTraceBasicSaleResponse = new PayTraceBasicSaleResponse();
			payTraceBasicSaleResponse = JsonSerializer.DeserializeResponse<PayTraceBasicSaleResponse>(tempResponse);

			//Assign the http error if any
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)payTraceBasicSaleResponse);

			//Return the Desearlized object
			return payTraceBasicSaleResponse;

		}
	}

}

