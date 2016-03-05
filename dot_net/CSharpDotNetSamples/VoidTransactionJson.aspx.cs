using System;
using System.Web;
using System.Web.UI;

namespace AspNetClientEncryptionExample
{
	
	public partial class TransactionVoidJson : System.Web.UI.Page
	{

		public void BtnVoidTransactionClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				VerifyOAuthToken(tokenGenerator.GetToken ());
			}
		}
	
		protected void VerifyOAuthToken(OAuthToken oAuthResult) 
		{

			/// <summary>
			/// Determines whether Oauth token is successful with the specified OAuthResult or not.
			/// </summary>
			if(oAuthResult.ErrorFlag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141 so use 'Bearer'
				string OAuth = String.Format ("Bearer {0}", oAuthResult.AccessToken);

				//Build Transaction
				BuildTransaction(OAuth);

			} 
			else // Error for OAuth
			{
                // Do you code here to handle the OAuth error

                // Display the OAuth Error - Optional
                Response.Write(" OAuth Token Request: " + "Failed!" + "<br>");
                Response.Write (" Http Status Code & Description : " +  oAuthResult.ObjError.HttpTokenError  + "<br>");
				Response.Write (" API Error : " +  oAuthResult.ObjError.Error + "<br>");
				Response.Write (" API Error Message : " +  oAuthResult.ObjError.ErrorDescription+ "<br>");

			}
		}


		public void BuildTransaction(string oAuth)
		{
			// Void Transaction Request
			VoidTransactionRequest  requestVoidTransaction = new VoidTransactionRequest ();

			// for void Transaction Request execuation 
			VoidTransactionGenerator VoidTransactionGenerator = new VoidTransactionGenerator();

			// Assign the values to the void Transaction Request.
			requestVoidTransaction = BuildRequestFromFields(requestVoidTransaction);

			// To make Void Transaction Request and store the response
			var result = VoidTransactionGenerator.VoidTransactionTrans(oAuth,requestVoidTransaction);

			//display the void Transaction Response
			WriteResults(result);

		}

		protected VoidTransactionRequest BuildRequestFromFields(VoidTransactionRequest requestVoidTransaction)
		{
			// Build void Transaction fields from the input source

			// Transaction_id value = Any unsettled Transaction ID.
			// Transaction_id should be collected from any previous API response which contains Transaction ID of any unsettled Transaction 
			requestVoidTransaction.TransactionId =  105067320;
			// requestVoidTransaction.transaction_id =  104541685;
			return requestVoidTransaction;

		}


		//Based on the response display the result.
		protected void WriteResults(PayTraceBasicResponse result) 
		{

			if(null != result.HttpErrorMessage  && result.Success == false )
			{
				Response.Write ("<br>" + "Http Error Code & Error : " + result.HttpErrorMessage + "<br>");

				Response.Write ("Success : " + result.Success + "<br>"); 
				Response.Write ("response_code : " + result.ResponseCode + "<br>");   
				Response.Write ("status_message : " + result.StatusMessage +  "<br>"); 

				//Check the actual API errors with appropriate code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.TransactionErrors) 
				{	
					// to read Error message with each error code in array.
					foreach (var errorMessage in (string[])item.Value) 
					{
						Response.Write (item.Key  + "=" + errorMessage + "<br>");
					}
				}
				//Optional
				Response.Write ("Void Transaction: " + "Failed!" + "<br>");	

			} 
			else
			{
				// Do your code when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				if (result.ResponseCode == 109 && result.Success == true ) //for transation successfully approved 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);

					//Optional
					Response.Write ("Void Transaction : " + "Success!" + "<br>");		

				}

				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verification - avs_response and csc_response

					//Display Response
					DisplaySaleResponse(result);

					//Optional : Provide Appropriate message/action
					Response.Write ("Error : " + result.HttpErrorMessage + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the void Transaction Response
		protected void DisplaySaleResponse(PayTraceBasicResponse result)
		{
			Response.Write ("<br>"+ "Success : " + result.Success + "<br>"); 
			Response.Write ("response_code : " + result.ResponseCode + "<br>");   
			Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
			Response.Write ("transaction_id : " + result.TransactionId + "<br>"); 

		}


	}
}

