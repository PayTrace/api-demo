using System;
using System.Web;
using System.Web.UI;
using System.Web.Script.Serialization ; 

namespace AspNetClientEncryptionExample
{
	
	public partial class KeyedSaleJson : System.Web.UI.Page
	{
		public void BtnKeyedSaleClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				IsOAuthTokenSuccessful(tokenGenerator.GetToken ());

				/* Ideal code for a merchant's website:
				PayTraceSession session = new PayTraceSession ();
				KeyedSaleRequest request = buildRequestFromFields ();
				PayTraceTransaction result = session.processTransaction (request);
				*/
			}
		}
			

		protected void IsOAuthTokenSuccessful(OAuthToken OAuthResult) 
		{

			if(OAuthResult.errorflag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141
				string OAuth = String.Format ("Bearer {0}", OAuthResult.access_token);

				// Key Sale Request
				KeyedSaleRequest requestKeyedSale = new KeyedSaleRequest ();

				//TODO: Provide appropriate comment - KeySale Transaction
				KeyedSaleGenerator keyedSaleGenerator = new KeyedSaleGenerator();
			
				// Assign the values to the key Sale Request.
				requestKeyedSale = BuildRequestFromFields(requestKeyedSale);

				// To make Keyed Sale Request and store the response
				var result = keyedSaleGenerator.KeyedSaleTrans(OAuth,requestKeyedSale);

				//display the Keyed Sale Response
				WriteResults(result);

			} 
			else // Error for OAuth
			{
				// do you code here to handle the OAuth error

				// Display the OAuth Error - Optional
				Response.Write (" Http Status Code & Description : " +  OAuthResult.Error.token_error_http  + "<br>");
				Response.Write (" API Error : " +  OAuthResult.Error.error + "<br>");
				Response.Write (" API Error Message : " +  OAuthResult.Error.error_description+ "<br>");
				Response.Write (" Token Request: " + "Failed!" + "<br>");
			}

		}

		protected KeyedSaleRequest BuildRequestFromFields(KeyedSaleRequest requestKeyedSale)
		{
			// Build Keyed Sale Request fields from the input source

			requestKeyedSale.amount = 1.0;

			requestKeyedSale.credit_card = new CreditCard ();
			requestKeyedSale.credit_card.number = "4111111111111111";
			requestKeyedSale.credit_card.expiration_month = "12";
			requestKeyedSale.credit_card.expiration_year = "2020";

			requestKeyedSale.csc = "999";

			requestKeyedSale.billing_address = new BillingAddress ();
			requestKeyedSale.billing_address.name = "Steve Smith";
			requestKeyedSale.billing_address.street_address = "8320 E. West St.";
			requestKeyedSale.billing_address.city = "Spokane";
			requestKeyedSale.billing_address.state = "WA";
			requestKeyedSale.billing_address.zip = "85284";

			return requestKeyedSale;
		
		}


		protected void WriteResults(TransactionResponse result) 
		{

			if(null != result.ErrorMsg  && result.success == false )
			{
				Response.Write ("Http Error Code & Error : " + result.ErrorMsg + "<br>");
						
				Response.Write ("Success : " + result.success + "<br>"); 
				Response.Write ("response_code : " + result.response_code + "<br>");   
				Response.Write ("status_message : " + result.status_message + "<br>"); 
				Response.Write ("external_transaction_id : " + result.external_transaction_id + "<br>"); 
				Response.Write ("masked_card_number : " + result.masked_card_number + "<br>"); 
				Response.Write ("Keyed sale: " + "Failed!" + "<br>");	

				//Check the actual API errors with appropriate code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.errors) 
				{	
					// to read Error message with each error code in array.
					foreach (var errorMessage in (string[])item.Value) 
					{
						Response.Write (item.Key  + "=" + errorMessage + "<BR>");
					}
				}
					
			} 
			else
			{
				// Do your code when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				if (result.response_code == 101 && result.success == true ) //for transation successfully approved 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);
					Response.Write ("Keyed sale: " + "Success!" + "<br>");		
				
				}
					
				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verification - avs_response and csc_response

					//Display Response
					DisplaySaleResponse(result);
					Response.Write ("Error : " + result.ErrorMsg + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the Keyed Sale Response
		protected void DisplaySaleResponse(TransactionResponse result)
		{
			Response.Write ("Success : " + result.success + "<br>"); 
			Response.Write ("response_code : " + result.response_code + "<br>");   
			Response.Write ("status_message : " + result.status_message + "<br>"); 
			Response.Write ("transaction_id : " + result.transaction_id + "<br>"); 
			Response.Write ("approval_code : " + result.approval_code + "<br>"); 
			Response.Write ("approval_message : " + result.approval_message + "<br>"); 
			Response.Write ("avs_response : " + result.avs_response + "<br>"); 
			Response.Write ("csc_response : " + result.csc_response + "<br>"); 
			Response.Write ("external_transaction_id : " + result.external_transaction_id + "<br>"); 
			Response.Write ("masked_card_number : " + result.masked_card_number + "<br>"); 
		}

	}
}

