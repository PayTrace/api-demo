using System;
using System.Net;
using System.IO;
using System.Text;
//this is for output display 
using System.Collections.Generic ;
using System.Web.Script.Serialization;

namespace AspNetClientEncryptionExample
{
	public class CreateCustomerProfileGenerator
	{


		public CreateCustomerProfileResponse CreateCustomerProfileTrans(string token, CreateCustomerProfileRequest createCustomerProfileRequest)
		{
			/// <summary>
			/// Method for builiding Transaction with Json Request,call the actual transaction execution method and call for Deseralize Json 
			/// and Return the object.
			/// Returns the KeyedSaleResponse Type 
			/// </summary>

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

		/*public CreateCustomerProfileResponse CreateCustomerProfileTrans(string token, CreateCustomerProfileRequest CreateCustomerProfileRequest)
		{

			// Header details are available at Authentication header page.
			string methodUrl = ApiEndPointConfiguration.UrlCreateCustomer ;

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(CreateCustomerProfileRequest);
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + requestJSON + "<br>");

			//call for actual request and response
			var objPayTraceResponse = new PayTraceResponse();
			var TempResponse = objPayTraceResponse.ProcessTransaction(methodUrl, token, requestJSON);

			return DeserializeResponse(TempResponse);
		}

		protected CreateCustomerProfileResponse DeserializeResponse(TempResponse TempResponse)
		{
			// Create an object to parse JSON data

			CreateCustomerProfileResponse ObjCreateCustomerProfileResponse= new CreateCustomerProfileResponse();
			var jsSerializer= new JavaScriptSerializer ();

			//optional - Display the Json Response
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + TempResponse.JsonResponse + "<br>");

			if (null != TempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				ObjCreateCustomerProfileResponse = jsSerializer.Deserialize<CreateCustomerProfileResponse>(TempResponse.JsonResponse);
			}
			ObjCreateCustomerProfileResponse.ErrorMsg = TempResponse.ErrorMessage;
			return ObjCreateCustomerProfileResponse;
		}*/
	}

}

