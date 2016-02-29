using System;
using System.Web;
using System.Web.UI;

namespace AspNetClientEncryptionExample
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

			if(oAuthResult.errorflag == false)
			{
				// In case of not using any OAuth2.0 Library
				// Use following when OAuth2.0 is caseinsesitive at Paytrace. 
				// string OAuth = String.Format ("{0} {1}", OAuthResult.token_type, OAuthResult.access_token);
				// For now OAuth2.0  is not caseinsesitive at PayTrace - ESC-141 so use 'Bearer'
				string OAuth = String.Format ("Bearer {0}", oAuthResult.access_token);

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
			Response.Write (" Http Status Code & Description : " +  OAuthResult.Error.token_error_http  + "<br>");
			Response.Write (" API Error : " +  OAuthResult.Error.error + "<br>");
			Response.Write (" API Error Message : " +  OAuthResult.Error.error_description+ "<br>");
			Response.Write (" Token Request: " + "Failed!" + "<br>");

		}

		protected CreateCustomerProfileRequest BuildRequestFromFields(CreateCustomerProfileRequest CreateCustomerProfileRequest)
		{
			// Build Keyed Sale Request fields from the input source

			CreateCustomerProfileRequest.customer_id = "customerTest1001";

			CreateCustomerProfileRequest.credit_card = new CreditCard ();
			CreateCustomerProfileRequest.credit_card.number = "5454545454545454";
			CreateCustomerProfileRequest.credit_card.expiration_month = "12";
			CreateCustomerProfileRequest.credit_card.expiration_year = "2020";

			CreateCustomerProfileRequest.billing_address = new BillingAddress ();
			CreateCustomerProfileRequest.billing_address.name = "Mike Smith";
			CreateCustomerProfileRequest.billing_address.street_address = "8230 E.Indiana St.";
			CreateCustomerProfileRequest.billing_address.city = "Spokane";
			CreateCustomerProfileRequest.billing_address.state = "WA";
			CreateCustomerProfileRequest.billing_address.zip = "85284";

			// optionl unless you have Discretionary data required.
			CreateCustomerProfileRequest.discretionary_data = new CustomerDiscretionaryData ();
			CreateCustomerProfileRequest.discretionary_data.TestingField = " Testing Discretionary Data 123";
			CreateCustomerProfileRequest.discretionary_data.Testing_DisData = "Testing_DisData 123";

			return CreateCustomerProfileRequest;

		}
		//Based on the response display the result.
		protected void WriteResults(CreateCustomerProfileResponse result) 
		{

			if(null != result.ErrorMsg  && result.success == false )
			{
				Response.Write ("<br>" + "Http Error Code & Error : " + result.ErrorMsg + "<br>");

				Response.Write ("Success : " + result.success + "<br>"); 
				Response.Write ("response_code : " + result.response_code + "<br>");   
				Response.Write ("status_message : " + result.status_message +  "<br>"); 
				Response.Write ("Masked_card_number : " + result.masked_card_number + "<br>");

				//Check the actual API errors with appropriate response code
				Response.Write (" API errors : "+ "<br>");
				foreach (var item in result.errors) 
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
				if (result.response_code == 160 && result.success == true ) 
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
					Response.Write ("Error : " + result.ErrorMsg + "<br>");

				}

				// Do your code for Any additional task !
			}
		}

		//Display the void Transaction Response
		protected void DisplaySaleResponse(CreateCustomerProfileResponse result)
		{
			Response.Write ("<br>"+ "Success : " + result.success + "<br>"); 
			Response.Write ("response_code : " + result.response_code + "<br>");   
			Response.Write ("status_message : " + result.status_message + "<br>"); 
			Response.Write ("Customer_id : " + result.customer_id + "<br>"); 
			Response.Write ("Masked_card_number : " + result.masked_card_number + "<br>"); 

		}


	}
}

