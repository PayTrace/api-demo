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
		public CreateCustomerProfileResponse CreateCustomerProfileTrans(string token, CreateCustomerProfileRequest CreateCustomerProfileRequest)
		{

			// Header details are available at Authentication header page.
			string methodUrl = "/v1/customer/create";

			var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			var requestJSON = jsSerializer.Serialize(CreateCustomerProfileRequest);

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
		}
	}

}

