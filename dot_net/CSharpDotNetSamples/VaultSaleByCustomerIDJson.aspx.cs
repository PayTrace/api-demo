using System;
using System.Web;
using System.Web.UI;

namespace AspNetClientEncryptionExample
{
	
	public partial class VaultSaleByCustomerIDJson : System.Web.UI.Page
	{

		public void BtnVaultSaleClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				VerifyOAuthToken(tokenGenerator.GetToken ());
			}
		}

		protected void VerifyOAuthToken(OAuthToken OAuthResult) 
		{

			if(OAuthResult.ErrorFlag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141 so use 'Bearer'
				string OAuth = String.Format ("Bearer {0}", OAuthResult.AccessToken);

				//Build Transaction
				BuildTransaction(OAuth);


			} 
			else // Error for OAuth
			{
				// Do you code here to handle the OAuth error

				// Optional - Display the OAuth Error 
				DisplayOAuthError(OAuthResult);
			}
		}

		public void BuildTransaction(string oAuth)
		{
			// Vault Sale by Customer Id Request
			VaultSaleByCustomerIdRequest  requestVaultSaleByCustomerId = new VaultSaleByCustomerIdRequest ();

			// for Vault Sale by Customer Id Transaction Request execuation 
			VaultSaleByCustomerIdGenerator  vaultSaleByCustomerIdGenerator  = new  VaultSaleByCustomerIdGenerator();

			// Assign the values to the void Transaction Request.
			requestVaultSaleByCustomerId = BuildRequestFromFields(requestVaultSaleByCustomerId);

			// To make Void Transaction Request and store the response
			var result = vaultSaleByCustomerIdGenerator.VaultSaleByCustomerIdTrans(oAuth,requestVaultSaleByCustomerId);

			// To Display the void Transaction Response
			WriteResults(result);
		}

		public void DisplayOAuthError(OAuthToken OAuthResult)
		{
            // Optional - Display the OAuth Error 
            Response.Write(" OAuth Token Request: " + "Failed!" + "<br>");
            Response.Write (" Http Status Code & Description : " +  OAuthResult.ObjError.HttpTokenError  + "<br>");
			Response.Write (" API Error : " +  OAuthResult.ObjError.Error + "<br>");
			Response.Write (" API Error Message : " +  OAuthResult.ObjError.ErrorDescription+ "<br>");
			
		}

		protected VaultSaleByCustomerIdRequest BuildRequestFromFields(VaultSaleByCustomerIdRequest requestVaultSaleByCustomerId)
		{
			// Build the Vault Sale by customerId fields from the input sources.
			// Customer ID can be obtained from any sources where it is stored previously. 
			// Strorage source could be at the PayTrace repository(if used create customer profile earlier) or at the client repository.  

			requestVaultSaleByCustomerId.amount = 0.99;
			requestVaultSaleByCustomerId.customer_id = "customerTest123";

			return requestVaultSaleByCustomerId;

		}

	

		protected void WriteResults(PayTraceBasicSaleResponse  result) 
		{
			
			// Based on the response display the result.

			if(null != result.HttpErrorMessage  && result.Success == false )
			{
				Response.Write ("<br>" + "Http Error Code & Error : " + result.HttpErrorMessage + "<br>");

				Response.Write ("Success : " + result.Success + "<br>"); 
				Response.Write ("response_code : " + result.ResponseCode + "<br>");   
				Response.Write ("status_message : " + result.StatusMessage +  "<br>"); 
				Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>");

				//Check the actual API errors with appropriate code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.TransactionErrors) 
				{	
					// to read Error message with each error code in an array.
					foreach (var errorMessage in (string[])item.Value) 
					{
						//Do your code here to handle an specific error based on the error key code 

						//Optional - display
						Response.Write (item.Key  + "=" + errorMessage + "<br>");
					}
				}
				//Optional
				Response.Write ("Vault Sale by Customer ID :  " + "Failed!" + "<br>" + "<br>");	

			} 
			else
			{
				// Do your code when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				// For transation successfully approved
				if (result.ResponseCode == 101 && result.Success == true )  
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);

					//Optional
					Response.Write ("Vault Sale by Customer ID :" + "Success!" + "<br>" + "<br>");		

				}

				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verification - avs_response and csc_response

					//Display Response
					DisplaySaleResponse(result);

					//Optional : Provide Appropriate message/action
					Response.Write ("Error : " + result.HttpErrorMessage + "<br>" + "<br>");

				}

				// Do your code for Any additional task!
			}
		}


		protected void DisplaySaleResponse(PayTraceBasicSaleResponse result)
		{

			//Display the Vault Sale by Customer ID Response

			Response.Write ("<br>"+ "Success : " + result.Success + "<br>"); 
			Response.Write ("response_code : " + result.ResponseCode + "<br>");   
			Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
			Response.Write ("transaction_id : " + result.TransactionId + "<br>"); 
			Response.Write ("approval_code : " + result.ApprovalCode + "<br>"); 
			Response.Write ("approval_message : " + result.ApprovalMessage + "<br>"); 
			Response.Write ("avs_response : " + result.AvsResponse + "<br>"); 
			Response.Write ("csc_response : " + result.CscResponse + "<br>"); 
			Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>"); 

		}

	}

}

