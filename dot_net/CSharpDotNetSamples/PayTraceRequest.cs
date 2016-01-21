using System;

namespace AspNetClientEncryptionExample
{


	// Class for credit card
	public class CreditCard  
	{
		// Declare 'encrypted_number' instead of 'number' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string number { get; set; } 
		public string expiration_month { get; set; }
		public string expiration_year { get; set; }
	}

	// Class for billing address
	public class BillingAddress 
	{
		public string name { get; set; }
		public string street_address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
	}

	// Class that holds basic Paytrace Sale/Refund request properties	
	public class SaleRequest 
	{
		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}

	public class KeyedSaleRequest 
	{
		/// <summary>
		/// Class for keyed sale request - 
		/// Please refer the account security page on PayTrace virtual Terminal to determine the property.
		/// </summary>
		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}

	public class SwipedSaleRequest
	{
		/// <summary>
		/// Class for swiped Sale Request 
		/// Please refer the account security page on PayTrace virtual Terminal to determine the property.
		/// </summary>

		public double amount { get; set; }
		//declare 'encrypted_swipe' instead of 'swipe' in case of using PayTrace client side encryption
		public string swipe { get; set; }  
	}


	public class KeyedRefundRequest 
	{
		/// <summary>
		/// this class holds properties for the KeyedRefund request.
		/// Please check the Account security settings before defining this class as there are some request fields are conditional and optional.
		/// this class uses Billing Address class 
		/// this class also uses Credit Card class
		/// </summary>
		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}
	public class VoidTransactionRequest
	{

		/// <summary>
		/// classr for void Transaction request
		/// </summary>
		public long transaction_id { get; set; }
	
	}


	// class for the holding oAuth Data 

	public class OAuthToken
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }

		//for Errors
		// Optional - flag for error
		public Boolean errorflag { get; set;}
		// Object for PayTrace Error Json Key
		public OAuthError Error { get; set; }
	}


	public class OAuthError
	{
		/// <summary>
		/// Class that holds Error for the OAuth token
		/// </summary>

		// Json key - returned by PayTrace API for error
		public string error { get; set; } 

		// Json key - returned by PayTrace API for error
		public string error_description { get; set; }

		// optional - for http error : 
		public string token_error_http{ get; set;}  
	}


}

