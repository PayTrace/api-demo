using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic ;
using System.Web.Script.Serialization;
using Newtonsoft.Json ;

namespace AspNetClientEncryptionExample
{
	public class JsonSerializer
	{
		public static string GetSeralizedString<T>(T obj) 
		{ 
			//var jsSerializer = new JavaScriptSerializer();

			//converting request into JSON string
			//var requestJSON = jsSerializer.Serialize(obj);

			var requestJSON = JsonConvert.SerializeObject(obj);	

			//Optional - Display Json Request 
			DisplayJsonRequest(requestJSON);

			return requestJSON; 
		}

		public static void AssignError(TempResponse tempResponse, PayTraceBasicResponse basicResponse)
		{
			basicResponse.ErrorMsg = tempResponse.ErrorMessage;
		}

		public static T DeserializeResponse<T>(TempResponse tempResponse)
		{ 
			T returnObject = default(T);

			//var jsSerializer= new JavaScriptSerializer ();

			//optional - Display the Json Response
			DisplayJsonResponse (tempResponse.JsonResponse);
		

			if (null != tempResponse.JsonResponse) 
			{
				// parse JSON data into C# obj
				//returnObject = jsSerializer.Deserialize<T>(tempResponse.JsonResponse);
				returnObject = JsonConvert.DeserializeObject<T>(tempResponse.JsonResponse);

			}

			return returnObject; 
		}

		public static void DisplayJsonRequest(string result)
		{
			//Optional - Display Json Request 
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Request: " + result + "<br>");
		}

		public static void DisplayJsonResponse(string result)
		{
			//optional - Display the Json Response
			System.Web.HttpContext.Current.Response.Write ("<br>" + "Json Response: " + result + "<br>");
		}
	}

}

