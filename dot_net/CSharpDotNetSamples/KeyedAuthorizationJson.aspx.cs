using System;
using System.Web;
using System.Web.UI;

namespace AspNetClientEncryptionExample
{
	
	public partial class KeyedAuthorizationJson : System.Web.UI.Page
	{
		public void BtnKeyedAuthorizationClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				//this is for genrating OAuth Token request
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
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141
				string OAuth = String.Format ("Bearer {0}", OAuthResult.AccessToken);

				//Build Transaction
				BuildTransaction(OAuth);

			} 
			else // Error for OAuth
			{
				// Do you code here to handle the token failure

				// Optional - Display the OAuth Error 
				DisplayOAuthError(OAuthResult);
			}
		}

		public void BuildTransaction(string oAuth)
		{
			// Keyed Authorization Request
			KeyedSaleRequest requestKeyedAuthorization = new KeyedSaleRequest ();

			// For Keyed Authorization Transaction request
			KeyedAuthorizationGenerator keyedAuthorizationGenerator = new KeyedAuthorizationGenerator();

			// Assign the values to the keyed Authorization Request.
			requestKeyedAuthorization = BuildRequestFromFields(requestKeyedAuthorization);

			// To make Keyed Authorization Request and store the response
			var result = keyedAuthorizationGenerator.KeyedAuthorizationTrans(oAuth,requestKeyedAuthorization);

			//display the Keyed Authorization Response
			WriteResults(result);

		}
		public void DisplayOAuthError(OAuthToken OAuthResult)
		{

            // Do you code here, in case of OAuth token failure
            // Optional - Display the OAuth Error 
            Response.Write ("OAuth Token Request " + "Failed!" + "<br>");
            Response.Write (" Http Status Code & Description : " +  OAuthResult.ObjError.HttpTokenError  + "<br>");
			Response.Write (" API Error : " +  OAuthResult.ObjError.Error + "<br>");
			Response.Write (" API Error Message : " +  OAuthResult.ObjError.ErrorDescription+ "<br>");
			

		}

		protected KeyedSaleRequest BuildRequestFromFields(KeyedSaleRequest requestKeyedSale)
		{
			// Build Keyed Sale Request fields from the input source

			requestKeyedSale.Amount = 4.50;

			requestKeyedSale.ObjCreditCard = new CreditCard ();
			requestKeyedSale.ObjCreditCard.CcNumber = "4111111111111111";
			requestKeyedSale.ObjCreditCard.ExpirationMonth = "12";
			requestKeyedSale.ObjCreditCard.ExpirationYear = "2019";

			// Commented assignments lines will give an error response
			//requestKeyedSale.credit_card.expiration_month = "13";
			//requestKeyedSale.credit_card.expiration_year = "2011";
			requestKeyedSale.Csc = "999";

			requestKeyedSale.ObjBillingAddress = new BillingAddress ();
			requestKeyedSale.ObjBillingAddress.Name = "Mike Smith";
			requestKeyedSale.ObjBillingAddress.StreetAddress = "8320 E. West St.";
			requestKeyedSale.ObjBillingAddress.City = "Spokane";
			requestKeyedSale.ObjBillingAddress.State = "WA";
			requestKeyedSale.ObjBillingAddress.Zip = "85284";

			return requestKeyedSale;

		}

		protected void WriteResults(KeyedSaleResponse result) 
		{

			if(null != result.HttpErrorMessage  && result.Success == false )
			{
				Response.Write ( "<br>" + "Http Error Code & Error : " + result.HttpErrorMessage + "<br>");

				Response.Write ("Success : " + result.Success + "<br>"); 
				Response.Write ("response_code : " + result.ResponseCode + "<br>");   
				Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
				Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>"); 
				Response.Write ("masked_card_number : " + result.MaskedCardNumber + "<br>"); 

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
				Response.Write ("Keyed Authorization: " + "Failed!" + "<br>");	

			} 
			else
			{
				// Do your code when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				if (result.ResponseCode == 101 && result.Success == true ) //for transation successfully approved 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);
					Response.Write ("Keyed Authorization: " + "Success!" + "<br>");		

				}

				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verification - avs_response and csc_response

					//Display Response
					DisplaySaleResponse(result);
					Response.Write ("Error : " + result.HttpErrorMessage + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the Keyed Authorization Response
		protected void DisplaySaleResponse(KeyedSaleResponse result)
		{
			Response.Write ( "<br>" + "Success : " + result.Success + "<br>"); 
			Response.Write ("response_code : " + result.ResponseCode + "<br>");   
			Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
			Response.Write ("transaction_id : " + result.TransactionId + "<br>"); 
			Response.Write ("approval_code : " + result.ApprovalCode + "<br>"); 
			Response.Write ("approval_message : " + result.ApprovalMessage + "<br>"); 
			Response.Write ("avs_response : " + result.AvsResponse + "<br>"); 
			Response.Write ("csc_response : " + result.CscResponse + "<br>"); 
			Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>"); 
			Response.Write ("masked_card_number : " + result.MaskedCardNumber + "<br>"); 
		}
	}
}

