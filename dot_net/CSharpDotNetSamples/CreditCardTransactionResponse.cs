using System;

namespace AspNetClientEncryptionExample
{
	public class CreditCardTransactionResponse
	{
		public CreditCardTransactionResponse ()
		{
		}
	}


	/*public class Errors
	{
		[System.Runtime.Serialization.DataMember("35")]
		public string ErrorCode35 { get; set; }

	}*/

	/*public class Errors
	{
		public List<string> ErrorCode { get; set; }

	}*/

	/*
	 * {
  "success": false,
  "response_code": 1,
  "status_message": "One or more errors has occurred.",
  "errors": 
    [
                { 
                    Code : 35,
                    Message : "Please provide a valid Credit Card Number."
                 },
             ],
  "external_transaction_id": "",
  "masked_card_number": "xxxxxxxxxxx1111"
}
	*/
}

