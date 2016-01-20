using System;
using System.Web;
using System.Web.UI;

namespace AspNetClientEncryptionExample
{
	
	public partial class KeyedRefundJson : System.Web.UI.Page
	{
		public void BtnKeyedRefundClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				IsOAuthTokenSuccessful(tokenGenerator.GetToken ());
			}
		}
		/// <summary>
		/// Determines whether this instance is O auth token successful the specified OAuthResult.
		/// </summary>
		/// <returns><c>true</c> if this instance is O auth token successful the specified OAuthResult; otherwise, <c>false</c>.</returns>
		/// <param name="OAuthResult">OAuthResult</param>
		protected void IsOAuthTokenSuccessful(OAuthToken OAuthResult) 
		{

			if(OAuthResult.errorflag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141 so use 'Bearer'
				string OAuth = String.Format ("Bearer {0}", OAuthResult.access_token);

				// Keyed Refund Request
				KeyedRefundRequest requestKeyedRefund = new KeyedRefundRequest();

				// for Keyed Refund Transaction
				KeyedRefundGenerator keyedRefundGenerator = new KeyedRefundGenerator();

				// Assign the values to the key Refund Request.
				requestKeyedRefund = BuildRequestFromFields(requestKeyedRefund);

				// To make Keyed Refund Request and store the response
				var result = keyedRefundGenerator.KeyedRefundTrans(OAuth,requestKeyedRefund);

				//display the Keyed Refund Response
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

		protected KeyedRefundRequest BuildRequestFromFields(KeyedRefundRequest requestKeyedRefund)
		{
			// Build Keyed Refund Request fields from the input source

			requestKeyedRefund.amount = 1.99;

			requestKeyedRefund.credit_card = new CreditCard ();
			requestKeyedRefund.credit_card.number = "5454545454545454";
			requestKeyedRefund.credit_card.expiration_month = "12";
			requestKeyedRefund.credit_card.expiration_year = "2018";

			requestKeyedRefund.csc = "999";

			requestKeyedRefund.billing_address = new BillingAddress ();
			requestKeyedRefund.billing_address.name = "Tom Smith";
			requestKeyedRefund.billing_address.street_address = "8320 E. West St.";
			requestKeyedRefund.billing_address.city = "Spokane";
			requestKeyedRefund.billing_address.state = "WA";
			requestKeyedRefund.billing_address.zip = "85284";

			return requestKeyedRefund;

		}

		//Based on the response display the result.
		protected void WriteResults(KeyedRefundResponse result) 
		{

			if(null != result.ErrorMsg  && result.success == false )
			{
				Response.Write ("Http Error Code & Error : " + result.ErrorMsg + "<br>");

				Response.Write ("Success : " + result.success + "<br>"); 
				Response.Write ("response_code : " + result.response_code + "<br>");   
				Response.Write ("status_message : " + result.status_message + "<br>"); 
				Response.Write ("external_transaction_id : " + result.external_transaction_id + "<br>"); 
				Response.Write("Masked_card_number : " + result.masked_card_number + "<br>");
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
				Response.Write ("Keyed Refund: " + "Failed!" + "<br>");	

			} 
			else
			{
				// Do your code when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				if (result.response_code == 106 && result.success == true ) //for transation successfully approved 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);
					Response.Write ("Keyed Refund: " + "Success!" + "<br>");		

				}

				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verification - avs_response and csc_response

					//Display Response
					DisplaySaleResponse(result);

					//optional : Provide Appropriate message/action
					Response.Write ("Error : " + result.ErrorMsg + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the Keyed Refund Response
		protected void DisplaySaleResponse(KeyedRefundResponse result)
		{
			Response.Write ("Success : " + result.success + "<br>"); 
			Response.Write ("response_code : " + result.response_code + "<br>");   
			Response.Write ("status_message : " + result.status_message + "<br>"); 
			Response.Write ("transaction_id : " + result.transaction_id + "<br>"); 
			Response.Write ("external_transaction_id : " + result.external_transaction_id + "<br>"); 
			Response.Write("Masked_card_number : " + result.masked_card_number + "<br>");
		}

	}
}

