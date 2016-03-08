using System;
using System.Web;
using System.Web.UI;

namespace CSharpDotNetJsonSample
{
	
	public partial class CreateCustomerJson : System.Web.UI.Page
	{
		public void BtnCreateCustomerClicked(object sender, EventArgs args)
		{
			if(this.IsPostBack) 
			{
				// To make a request for the OAuth Token
				OAuthTokenGenerator tokenGenerator = new OAuthTokenGenerator ();
				VerifyOAuthToken(tokenGenerator.GetToken ());
			}
		}


		protected void VerifyOAuthToken(OAuthToken oAuthResult) 
		{
			/// <summary>
			/// Determines whether OAuthToken is successful and build transaction
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
				// Do you code here to handle the token failure

				// Optional - Display the OAuth Error 
				DisplayOAuthError(oAuthResult);

			}

		}

		public void BuildTransaction(string oAuth)
		{
			// Create Customer Profile Request
			CreateCustomerProfileRequest  requestCreateCustomer = new CreateCustomerProfileRequest ();

			// for Create customer Request execuation 
			CreateCustomerProfileGenerator CreateCustomerProfileGenerator= new CreateCustomerProfileGenerator();

			// Assign the values to the Create Customer Request.
			requestCreateCustomer = BuildRequestFromFields(requestCreateCustomer);

			// To make a Create Customer Profile Request and store the response
			var result = CreateCustomerProfileGenerator.CreateCustomerProfileTrans(oAuth,requestCreateCustomer);

			//display the Create Customer Profile Response
			WriteResults(result);

		}

		public void DisplayOAuthError(OAuthToken OAuthResult)
		{

            // Do you code here, in case of OAuth token failure
           
            // Optional - Display the OAuth Error 
            Response.Write (" OAuth Token Request " + "Failed!" + "<br>");
            Response.Write (" Http Status Code & Description : " +  OAuthResult.ObjError.HttpTokenError  + "<br>");
			Response.Write (" API Error : " +  OAuthResult.ObjError.Error + "<br>");
			Response.Write (" API Error Message : " +  OAuthResult.ObjError.ErrorDescription+ "<br>");
			

		}

		protected CreateCustomerProfileRequest BuildRequestFromFields(CreateCustomerProfileRequest CreateCustomerProfileRequest)
		{
            // Build Keyed Sale Request fields from the input source

            //Provide unique customer ID everytime for successful request
            CreateCustomerProfileRequest.CustomerId = "customerTest2Demo";

			CreateCustomerProfileRequest.ObjCreditCard = new CreditCard ();
			CreateCustomerProfileRequest.ObjCreditCard.CcNumber = "5454545454545454";
			CreateCustomerProfileRequest.ObjCreditCard.ExpirationMonth = "12";
			CreateCustomerProfileRequest.ObjCreditCard.ExpirationYear = "2020";

			CreateCustomerProfileRequest.ObjBillingAddress = new BillingAddress ();
			CreateCustomerProfileRequest.ObjBillingAddress.Name = "Mike Smith";
			CreateCustomerProfileRequest.ObjBillingAddress.StreetAddress = "8230 E.Indiana St.";
			CreateCustomerProfileRequest.ObjBillingAddress.City = "Spokane";
			CreateCustomerProfileRequest.ObjBillingAddress.State = "WA";
			CreateCustomerProfileRequest.ObjBillingAddress.Zip = "85284";

			// optionl unless you have Discretionary data required.
			//CreateCustomerProfileRequest.discretionary_data = new CustomerDiscretionaryData ();
			//CreateCustomerProfileRequest.discretionary_data.TestingField = " Testing Discretionary Data 123";
			//CreateCustomerProfileRequest.discretionary_data.Testing_DisData = "Testing_DisData 123";

			return CreateCustomerProfileRequest;

		}
		//Based on the response display the result.
		protected void WriteResults(CreateCustomerProfileResponse result) 
		{

			if(null != result.HttpErrorMessage  && result.Success == false )
			{
				Response.Write ("<br>" + "Http Error Code & Error : " + result.HttpErrorMessage + "<br>");

				Response.Write ("Success : " + result.Success + "<br>"); 
				Response.Write ("response_code : " + result.ResponseCode + "<br>");   
				Response.Write ("status_message : " + result.StatusMessage +  "<br>"); 
				Response.Write ("Masked_card_number : " + result.MaskedCardNumber + "<br>");

				//Check the actual API errors with appropriate response code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.TransactionErrors) 
				{	
					// to read Error message with each error code in array.
					foreach (var errorMessage in (string[])item.Value) 
					{
						Response.Write (item.Key  + "=" + errorMessage + "<BR>");
					}
				}
				//Optional
				Response.Write ("Create Customer Profile " + "Failed!" + "<br>");	

			} 
			else
			{
				// Do your code when Response is available based on the response_code. 
				// Please refer PayTrace-HTTP Status and Error Codes page for possible errors and Response Codes
				// When Customer profile is created successfully. 
				if (result.ResponseCode == 160 && result.Success == true ) 
				{
					// Do you code for any additional verification

					// Display Response - optional
					DisplaySaleResponse(result);

					//Optional
					Response.Write ("Create Customer Profile  " + "Success!" + "<br>");		

				}

				else
				{
					// Do your code here based on the response_code - use the PayTrace http status and error page for reference
					// Do your code for any additional verification - avs_response and csc_response

					//optional - Display Response
					DisplaySaleResponse(result);

					//Optional - Provide Appropriate message/action
					Response.Write ("Error : " + result.HttpErrorMessage + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the void Transaction Response
		protected void DisplaySaleResponse(CreateCustomerProfileResponse result)
		{
			Response.Write ("<br>"+ "Success : " + result.Success + "<br>"); 
			Response.Write ("response_code : " + result.ResponseCode + "<br>");   
			Response.Write ("status_message : " + result.StatusMessage + "<br>"); 
			Response.Write ("Customer_id : " + result.CustomerId + "<br>"); 
			Response.Write ("Masked_card_number : " + result.MaskedCardNumber + "<br>"); 

		}


	}
}

