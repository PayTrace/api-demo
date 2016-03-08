using System;
using System.Web;
using System.Web.UI;

namespace CSharpDotNetJsonSample
{
	
	public partial class CaptureJson : System.Web.UI.Page
	{
		public void BtnCaptureClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				//this is for genrating OAuth Token request
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				VerifyOAuthToken(tokenGenerator.GetToken ());
			}
		}

		protected void VerifyOAuthToken(OAuthToken oAuthResult) 
		{
			/// <summary>
			/// Determines whether OAuthToken is successful and make a request
			/// </summary>

			if(oAuthResult.ErrorFlag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141 so use 'Bearer'
				string OAuth = String.Format ("Bearer {0}", oAuthResult.AccessToken);

				// Build a Transaction 
				BuildTransaction (OAuth);

			} 
			else // Error for OAuth
			{
				// Do you code here to handle the OAuth error

				// Optional - Display the OAuth Error 
				DisplayOAuthError(oAuthResult);
			}
		}

		public void BuildTransaction(string oAuth)
		{
			// Capture Transaction Request
			CaptureTransactionRequest  requestCaptureTransaction = new CaptureTransactionRequest ();

			// for Cature Transaction Request execuation 
			CaptureTransactionGenerator CaptureTransactionGenerator = new CaptureTransactionGenerator();

			// Assign the values to the void Transaction Request.
			requestCaptureTransaction = BuildRequestFromFields(requestCaptureTransaction);

			// To make Void Transaction Request and store the response
			var result = CaptureTransactionGenerator.CaptureTransactionTrans(oAuth,requestCaptureTransaction);

			//display the void Transaction Response
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
		protected CaptureTransactionRequest BuildRequestFromFields(CaptureTransactionRequest requestCaptureTransaction)
		{
            // Build Capture Transaction fields from the input sources.
            // Transaction_id should be collected from previously Authorised API response - Transaction ID or if you have stored anywhere in DB or in any Variables. 
            // Optional - it shows how to get value from the form text box.
            
            // requestCaptureTransaction.Amount= 1.0 ;

            
            var transactionId = Request.Form["TransactionNumber"];

            //following IF statement is optional if you already have input validation for Empty Transaction number textbox
            if (string.IsNullOrEmpty(transactionId))
            {
                transactionId = new Random().Next().ToString();
            }

            requestCaptureTransaction.TransactionId = Convert.ToInt64(transactionId);
         
			return requestCaptureTransaction;

		}
			
		//Based on the response display the result.
		protected void WriteResults(PayTraceExternalTransResponse  result) 
		{

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
					// to read Error message with each error code in array.
					foreach (var errorMessage in (string[])item.Value) 
					{
						//Do your code here to handle an specific error based on the error key code 

						//Optional - display
						Response.Write (item.Key  + "=" + errorMessage + "<br>");
					}
				}
				//Optional
				Response.Write ("<br>" + "Capture Transaction: " + "Failed!" + "<br>");	

			} 
			else
			{
				// Do your code when Response is available based on the response_code. 

				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				if (result.ResponseCode == 112 && result.Success == true ) //for transation successfully approved 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);

					//Optional
					Response.Write ("Capture Transaction : " + "Success!" + "<br>");		

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
		protected void DisplaySaleResponse(PayTraceExternalTransResponse result)
		{
			Response.Write ("<br>"+ "Success : " + result.Success + "<br>"); 
			Response.Write ("response_code : " + result.ResponseCode + "<br>");   
			Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
			Response.Write ("transaction_id : " + result.TransactionId + "<br>"); 
			Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>");
		}

	}
}

