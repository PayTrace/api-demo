using System;
using Newtonsoft.Json;


namespace AspNetClientEncryptionExample
{
    /// <summary>
    /// class that contains API username and password for requesting OAuthToken
    /// Replace the username and password with your assigned API user credentials.
    /// </summary>
    public class ApiAccessCredentials
    {
        public static string GrantType = "password";
        public static string UserName = "demo123";
        public static string Password = "demo123";

        public static string GetFormattedRequest()
        {
            return string.Format("grant_type={0}&username={1}&password={2}", GrantType, UserName,Password);
        }
    }
    
    /// <summary>
	/// Class contains base url and all request URLs 
	/// Please Refer PayTrace API Methods for the request url detail
    /// Add the new ones as you add new methods 
	/// </summary>
	public class ApiEndPointConfiguration
	{
		/// <summary>
		/// BaseUrl contains PayTrace API URL
		/// </summary>
		public const string BaseUrl = "https://api.paytrace.com"; //Production. 

		/// <summary>
		/// APIVersion contains Version of API 
		/// </summary>
		public const string ApiVersion = "/v1";

		/// <summary>
		/// Url for OAuth Token 
		/// </summary>
		public const string UrlOAuth = "/oauth/token";

		/// <summary>
		/// URL for Keyed Sale
		/// </summary>
		public const string UrlKeyedSale = ApiVersion + "/transactions/sale/keyed";

		/// <summary>
		/// URL for Swiped Sale
		/// </summary>
		public const string UrlSwipedSale = ApiVersion + "/transactions/sale/swiped";

		/// <summary>
		/// URL for Keyed Authorization
		/// </summary>
		public const string UrlKeyedAuthorization = ApiVersion + "/transactions/authorization/keyed";

		/// <summary>
		/// URL for Keyed Refund
		/// </summary>
		public const string UrlKeyedRefund = ApiVersion + "/transactions/refund/keyed";

		/// <summary>
		/// URL for Capture Transaction
		/// </summary>
		public const string UrlCapture= ApiVersion + "/transactions/authorization/capture";

		/// <summary>
		/// URL for Void Sale Transaction
		/// </summary>
		public const string UrlVoidTransaction = ApiVersion + "/transactions/void";

		/// <summary>
		/// URL for Vault Sale by CustomerId Method
		/// </summary>
		public const string UrlCreateCustomer = ApiVersion + "/customer/create";

		/// <summary>
		/// URL for Vault Sale by CustomerId Method
		/// </summary>
		public const string UrlVaultSaleByCustomerId = ApiVersion + "/transactions/sale/by_customer";

	}

    /// <summary>
    /// Class for credit card
    /// </summary>
    public class CreditCard  
	{

        // Declare 'encrypted_number' instead of 'number' in case of using PayTrace Client-Side Encryption JavaScript Library.
        [JsonProperty("number")]
        public string CcNumber { get; set; }

        [JsonProperty("expiration_month")]
        public string ExpirationMonth { get; set; }

        [JsonProperty("expiration_year")]
        public string ExpirationYear { get; set; }
	}

    /// <summary>
    /// Class for billing address
    /// </summary>
    public class BillingAddress 
	{
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
	}

    /// <summary>
    /// Class for keyed sale request and Keyed Authorization.
    /// Please refer the account security page on PayTrace virtual Terminal to determine the property.
    /// </summary>
    public class KeyedSaleRequest 
	{
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("credit_card")]
        public CreditCard ObjCreditCard { get; set; }
        // Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.

        [JsonProperty("csc")]
        public string Csc { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress ObjBillingAddress { get; set; }
	}

    /// <summary>
    /// Class for swiped Sale Request. 
    /// Please refer the account security page on PayTrace virtual Terminal to determine the property.
    /// </summary>
    public class SwipedSaleRequest
	{
        [JsonProperty("amount")]
        public double Amount { get; set; }

        //declare 'encrypted_swipe' instead of 'swipe' in case of using PayTrace client side encryption
        //this will include both track1 and track2 data
        [JsonProperty("swipe")]
        public string SwipeCcData { get; set; }  
	}

    /// <summary>
    /// This class holds properties for the KeyedRefund request.
    /// Please check the Account security settings before defining this class as there are some request fields are conditional and optional.
    /// This class uses Billing Address class.
    /// This class also uses Credit Card class.
    /// </summary>
    public class KeyedRefundRequest 
	{
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("credit_card")]
        public CreditCard ObjCreditCard { get; set; }
        // Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.

        [JsonProperty("csc")]
        public string Csc { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress ObjBillingAddress { get; set; }
	}

	public class VoidTransactionRequest
    {
        [JsonProperty("transaction_id")]
        public long TransactionId { get; set; }

	}
    /// <summary>
    /// classr for Capture Transaction request - include other optional inputs from the PayTrace Capture page as needed.
    /// </summary>
    public class CaptureTransactionRequest
	{
        // uncomment amount if your requirement is to send the amount with capture and make relavant changes
        [JsonProperty("amount")]
        public double Amount {get; set; } 

        [JsonProperty("transaction_id")]
        public long TransactionId { get; set; }

	}

    /// <summary>
    /// Class for Vault Sale by Customer ID request
    /// Include other optional inputs from the PayTrace Capture page as needed.
    /// </summary>
    public class VaultSaleByCustomerIdRequest
    {
        [JsonProperty("amount")]
        public double Amount {get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

	}
    /// <summary>
    /// Class for Create Customer Profile request 
    /// Please refer the account security page on PayTrace virtual Terminal to determine the properties and Create Customer Profile Page.
    /// </summary>	
    public class CreateCustomerProfileRequest 
	{

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("credit_card")]
        public CreditCard ObjCreditCard { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress ObjBillingAddress { get; set; }
		
        /// <summary>
		/// This Discretionary_data object is optionl - declare it in case you have discretionary data requiered for the customer
		/// Those can be set from the PayTrace Virtual Terminal - Discretionary Data 
		/// </summary>
		//public CustomerDiscretionaryData discretionary_data { get; set; }

	}

	public class CustomerDiscretionaryData
	{
		/// <summary>
		/// This class holds properties for the Customer - Discretionary data 
		/// Properties name should be same as Discretionary Data field names - as selected from the PayTrace Virtual Terminals
		/// </summary>
		public string TestingField { get; set; }

        [JsonProperty("Testing_DisData")]
        public string Testing_DisData { get; set; }

	}





}

