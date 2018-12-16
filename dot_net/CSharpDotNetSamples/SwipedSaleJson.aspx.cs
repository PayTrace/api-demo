using System;
using System.Web;
using System.Web.UI;

namespace CSharpDotNetJsonSample
{
	
	public partial class SwipedSaleJson : System.Web.UI.Page
	{
		public void BtnSwipedSaleClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				VerifyOAuthToken(tokenGenerator.GetToken ());
			}
		}


		protected void VerifyOAuthToken(OAuthToken oAuthResult) 
		{

			if(oAuthResult.ErrorFlag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0 is not caseinsesitive at PayTrace - ESC-141

				string OAuth = String.Format ("Bearer {0}", oAuthResult.AccessToken);
			
				//Build Transaction
				BuildTransaction(OAuth);

			} 
			else 
			{
				// Error for OAuth
				// Do your own code here to handle the error

				// Display the OAuth Error - Optional
				DisplayOAuthError(oAuthResult);
			}

		}

		public void BuildTransaction(string oAuth)
		{
			// Swiped Sale Request
			SwipedSaleRequest swipeSaleRequest = new SwipedSaleRequest();

			//Swiped Sale Transaction
			SwipedSaleGenerator swipedSaleGenerator = new SwipedSaleGenerator();

			//Assign the values to the Swiped Sale Request.
			swipeSaleRequest = BuildRequestFromFields(swipeSaleRequest);

			//Swiped Sale Request and display the result
			var result = swipedSaleGenerator.SwipedSaleTrans(oAuth,swipeSaleRequest);

			//process the Swiped Sale response
			WriteResults(result);

		}

		public void DisplayOAuthError(OAuthToken OAuthResult)
		{
			// Optional - Display the OAuth Error

            Response.Write ("  OAuthToken Request: " + "Failed!" + "<br>"); 
			Response.Write (" Http Status Code & Description : " +  OAuthResult.ObjError.HttpTokenError  + "<br>");
			Response.Write (" API Error : " +  OAuthResult.ObjError.Error + "<br>");
			Response.Write (" API Error Message : " +  OAuthResult.ObjError.ErrorDescription+ "<br>");

		}

		protected SwipedSaleRequest BuildRequestFromFields(SwipedSaleRequest requestSwipedSale) 
		{
			// Build Keyed Sale Request fields from the input source
			requestSwipedSale.Amount = 4.00;

			//Swipe value should be detected from Magnetic stripe reader(Credit Card reader Device) 
			//this will include both track1 and track2 data
			 requestSwipedSale.SwipeCcData = "%B4012881888818888^Demo/Customer^2412101001020001000000701000000?;4012881888818888=24121010010270100001?";
			//Following value will give an error 
			//requestSwipedSale.swipe = "012881888818888^Demo/Customer^2412101001020001000000701000000?;4012881888818888=24121010010270100001?";

			return requestSwipedSale;

		}

		protected void WriteResults(PayTraceBasicSaleResponse result) 
		{

			if(null != result.HttpErrorMessage  && result.Success == false )
			{
				Response.Write ("<br>"+ "Http Error Code & Error : " + result.HttpErrorMessage + "<br>");

				Response.Write ("Success : " + result.Success + "<br>"); 
				Response.Write ("response_code : " + result.ResponseCode + "<br>");   
				Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
				Response.Write ("external_transaction_id : " + result.ExternalTransactionId + "<br>"); 

				// Check the actual API errors with appropriate code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.TransactionErrors) 
				{
					foreach (var errorMessage in (string[])item.Value) // to read Error message with each code in array.
					{
						Response.Write (item.Key  + "=" + errorMessage + "<BR>");
					}
				}
				Response.Write ("Swiped Sale : " + "Failed!" + "<br>");	
			} 
			else
			{
				// Do your code here when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				// for transaction successfully approved 
				if (result.ResponseCode == 101 && result.Success == true ) 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);
					Response.Write ("Swiped Sale : " + "Success!" + "<br>");		

				}
				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verifications - avs_response and csc_response

					//Display Response - optionals
					DisplaySaleResponse(result);
					Response.Write ("Error : " + result.HttpErrorMessage + "<br>");

				}
				// Do your code for Any additional task !
			}
		}


		protected void DisplaySaleResponse(PayTraceBasicSaleResponse result)
		{
			// Display the Swiped Sale Response
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

