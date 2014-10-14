using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using DotNetOpenAuth;
using DotNetOpenAuth.OAuth2;
using Newtonsoft.Json;
using System.Collections;

namespace PayTrace.Hermes
{
    class Example
    {
       // static const String APIURL = "https://apitest2.paytrace.com/";216.109.134.22:8001

        // should be able to use the url above as well.
         const String APIURL = "https://216.109.134.22:4343";

         static void Main(string[] args)
         {
             
             // Remove this for production it just eats tee  Https invalid certificate error
             ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

             // usung DotNetOpenAuth lib here but any Auth2 lib should work
             var authServer = new AuthorizationServerDescription
             {
                 TokenEndpoint = new Uri(APIURL + "/oauth/token"),
                 ProtocolVersion = ProtocolVersion.V20
             };

             // get our token 
             var client = new WebServerClient(authServer, APIURL + "/oauth/token");
             var auth = client.ExchangeUserCredentialForToken("demo123", "demo123");  
             Console.WriteLine(String.Format("Access Token Set to: {0} ", auth.AccessToken));

             // create credit keysale object and transform to json
             var keySale = new { amount = 19.95, credit_card = new { number = "4111111111111111", expiration_month = 12, expiration_year = 20 } };
             var json = JsonConvert.SerializeObject(keySale);

             Console.WriteLine(Environment.NewLine + string.Format("Json Card String: {0}", json));
             
             byte[] buf = Encoding.UTF8.GetBytes(json);
            
             // build request
             var request = (HttpWebRequest)WebRequest.Create(APIURL + "/v1/transactions/sale/keyed");
             request.ContentType = @"application/json";
             request.Method = "POST";
             request.ContentLength = buf.Length;
                
             // add our auth token 
             client.AuthorizeRequest(request, auth);
            // buffer the body
            request.GetRequestStream().Write(buf, 0, buf.Length);

            try
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Response: ");
                Console.WriteLine(Environment.NewLine);
                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    Console.WriteLine(reader.ReadToEnd());
                    Console.WriteLine(Environment.NewLine);

                }
            }
            catch (WebException e)
            {
                Console.WriteLine(string.Format("Error: {0}", e.Message));
                Console.WriteLine(Environment.NewLine);
            }
            finally
            {
                Console.WriteLine(Environment.NewLine + "Hit any key to quit: ");
                Console.Read();
            }
            


         }
       
    }
}
