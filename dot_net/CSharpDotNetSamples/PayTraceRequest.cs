using System;

namespace AspNetClientEncryptionExample
{
	// class that holds basic Paytrace Sale/Refund request properties

	// Class for credit card


	// Class that holds basic Paytrace Sale/Refund request properties	
	public class SaleRequest 
	{
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

	public class PayTraceRequest
	{
		
	}


}

