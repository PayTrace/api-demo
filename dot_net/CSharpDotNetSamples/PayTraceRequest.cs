using System;

namespace AspNetClientEncryptionExample
{
	
	public class CreditCard  
	{
		/// <summary>
		///  Class for credit card
		/// </summary>
		// Declare 'encrypted_number' instead of 'number' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string number { get; set; } 
		public string expiration_month { get; set; }
		public string expiration_year { get; set; }
	}

	public class BillingAddress 
	{
		/// <summary>
		/// Class for billing address
		/// </summary>

		public string name { get; set; }
		public string street_address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
	}


	/*public class SaleRequest 
	{
		/// <summary>
		/// Class that holds basic Paytrace Sale/Refund request properties	
		/// you can use this class instead of all the individaul class for request - however this depends on your security settings from PayTrace Terminal 
		/// </summary>

		public double amount { get; set; }
		public CreditCard credit_card { get; set; } 
		// Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript Library.
		public string csc { get; set; } 
		public BillingAddress billing_address { get; set; }
	}*/

	public class KeyedSaleRequest 
	{
		/// <summary>
		/// Class for keyed sale request and Keyed Authorization.
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
		/// Class for swiped Sale Request. 
		/// Please refer the account security page on PayTrace virtual Terminal to determine the property.
		/// </summary>

		public double amount { get; set; }
		//declare 'encrypted_swipe' instead of 'swipe' in case of using PayTrace client side encryption
		public string swipe { get; set; }  
	}

	public class KeyedRefundRequest 
	{
		/// <summary>
		/// This class holds properties for the KeyedRefund request.
		/// Please check the Account security settings before defining this class as there are some request fields are conditional and optional.
		/// This class uses Billing Address class .
		/// This class also uses Credit Card class.
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
		/// class for void Transaction request
		/// </summary>
		public long transaction_id { get; set; }

	}
	public class CaptureTransactionRequest
	{

		/// <summary>
		/// classr for Capture Transaction request - include other optional inputs from the PayTrace Capture page as needed.
		/// </summary>
		//public double amount {get; set; }
		public long transaction_id { get; set; }

	}

	public class CreateCustomerProfileRequest 
	{
		/// <summary>
		/// Class for Create Customer Profile request 
		/// Please refer the account security page on PayTrace virtual Terminal to determine the properties and Create Customer Profile Page.
		/// </summary>
		public string customer_id { get; set; }
		public CreditCard credit_card { get; set; } 
 		public BillingAddress billing_address { get; set; }
		/// <summary>
		/// This Discretionary_data object is optionl - declare it in case you have discretionary data requiered for the customer
		/// Those can be set from the PayTrace Virtual Terminal - Discretionary Data
		/// </summary>
		public CustomerDiscretionaryData discretionary_data { get; set; }

	}
	public class CustomerDiscretionaryData
	{
		/// <summary>
		/// This class holds properties for the Customer - Discretionary data 
		/// Properties name should be same as Discretionary Data field names - as selected from the PayTrace Virtual Terminals
		/// </summary>
		public string TestingField { get; set; }
		public string Testing_DisData { get; set; }

	}





}

