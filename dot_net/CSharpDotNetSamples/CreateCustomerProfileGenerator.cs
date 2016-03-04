using System;
using System.Net;
using System.IO;
using System.Text;
//this is for output display 
using System.Collections.Generic ;


namespace AspNetClientEncryptionExample
{
	public class CreateCustomerProfileGenerator
	{
        /// <summary>
        /// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
        /// and Return the object.
        /// Returns the CreateCustomerProfileResponse Type 
        /// </summary>
        public CreateCustomerProfileResponse CreateCustomerProfileTrans(string token, CreateCustomerProfileRequest createCustomerProfileRequest)
	 	 {

			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlCreateCustomer ;

			//converting request into JSON string
			var requestJSON = JsonSerializer.GetSeralizedString(createCustomerProfileRequest);

			//Optional - Display Json Request 
			//System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var payTraceResponse = new PayTraceResponse();
			var tempResponse = payTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			//Create and assign the deseralized object to appropriate response type
			var createCustomerProfileResponse = new CreateCustomerProfileResponse();
			createCustomerProfileResponse = JsonSerializer.DeserializeResponse<CreateCustomerProfileResponse>(tempResponse);

			//Assign the http error if any
			JsonSerializer.AssignError(tempResponse,(PayTraceBasicResponse)createCustomerProfileResponse);

			//Return the Desearlized object
			return createCustomerProfileResponse;
		}

	}

}

