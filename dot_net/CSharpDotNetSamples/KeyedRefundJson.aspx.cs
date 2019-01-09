using System;
using System.Web;
using System.Web.UI;

namespace CSharpDotNetJsonSample
{
	
	public partial class KeyedRefundJson : System.Web.UI.Page
	{
		public void BtnKeyedRefundClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				VerifyOAuthToken(tokenGenerator.GetToken ());
			}
		}

		protected void VerifyOAuthToken(OAuthToken OAuthResult) 
		{
			/// <summary>
			/// Determines whether OAuthToken is successful and make a request
			/// </summary>

			if(OAuthResult.ErrorFlag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141 so use 'Bearer'
				string OAuth = String.Format ("Bearer {0}", OAuthResult.AccessToken);

				// Build a Transaction 
				BuildTransaction (OAuth);

			} 
			else // Error for OAuth
			{
				// do you code here to handle the OAuth error

				// Display the OAuth Error - Optional
				DisplayOAuthError(OAuthResult);
			}

		}

		public void BuildTransaction(string oAuth)
		{
			// Keyed Refund Request
			KeyedRefundRequest requestKeyedRefund = new KeyedRefundRequest();

			// for Keyed Refund Transaction
			KeyedRefundGenerator keyedRefundGenerator = new KeyedRefundGenerator();

			// Assign the values to the key Refund Request.
			requestKeyedRefund = BuildRequestFromFields(requestKeyedRefund);

			// To make Keyed Refund Request and store the response
			var result = keyedRefundGenerator.KeyedRefundTrans(oAuth,requestKeyedRefund);

			//display the Keyed Refund Response
			WriteResults(result);

		}


		public void DisplayOAuthError(OAuthToken OAuthResult)
		{
            // Optional - Display the OAuth Error 
            Response.Write (" OAuth Token Request: " + "Failed!" + "<br>");
            Response.Write (" Http Status Code & Description : " +  OAuthResult.ObjError.HttpTokenError  + "<br>");
			Response.Write (" API Error : " +  OAuthResult.ObjError.Error + "<br>");
			Response.Write (" API Error Message : " +  OAuthResult.ObjError.ErrorDescription+ "<br>");
			

		}

		protected KeyedRefundRequest BuildRequestFromFields(KeyedRefundRequest requestKeyedRefund)
		{
			// Build Keyed Refund Request fields from the input source

			requestKeyedRefund.Amount = 1.99;

			requestKeyedRefund.ObjCreditCard = new CreditCard ();
			requestKeyedRefund.ObjCreditCard.CcNumber = "5454545454545454";
			requestKeyedRefund.ObjCreditCard.ExpirationMonth = "12";
			requestKeyedRefund.ObjCreditCard.ExpirationYear = "2018";

			requestKeyedRefund.Csc = "999";

			requestKeyedRefund.ObjBillingAddress = new BillingAddress ();
			requestKeyedRefund.ObjBillingAddress.Name = "Tom Smith";
			requestKeyedRefund.ObjBillingAddress.StreetAddress = "8320 E. West St.";
			requestKeyedRefund.ObjBillingAddress.City = "Spokane";
			requestKeyedRefund.ObjBillingAddress.State = "WA";
			requestKeyedRefund.ObjBillingAddress.Zip = "85284";

			return requestKeyedRefund;

		}

		//Based on the response display the result.
		protected void WriteResults(KeyedRefundResponse result) 
		{

			if(null != result.HttpErrorMessage  && result.Success == false )
			{
				Response.Write ("<br>" + "Http Error Code & Error : " + result.HttpErrorMessage + "<br>");

				Response.Write ("Success : " + result.Success + "<br>"); 
				Response.Write ("response_code : " + result.ResponseCode + "<br>");   
				Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
				Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>"); 
				Response.Write("Masked_card_number : " + result.MaskedCardNumber + "<br>");
				//Check the actual API errors with appropriate code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.TransactionErrors) 
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
				if (result.ResponseCode == 106 && result.Success == true ) //for transaction successfully approved 
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
					Response.Write ("Error : " + result.HttpErrorMessage + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the Keyed Refund Response
		protected void DisplaySaleResponse(KeyedRefundResponse result)
		{
			Response.Write ( "<br>" + "Success : " + result.Success + "<br>"); 
			Response.Write ("response_code : " + result.ResponseCode + "<br>");   
			Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
			Response.Write ("transaction_id : " + result.TransactionId + "<br>"); 
			Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>"); 
			Response.Write("Masked_card_number : " + result.MaskedCardNumber + "<br>");
		}

	}
}

