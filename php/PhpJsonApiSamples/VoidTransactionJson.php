<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title></title>
    </head>
    <body>
    <br>
           <a href="Default.html">Back to Home </a>  
        <br>
<?php
/*
 *This page will show you php sample for Void Transaction.
 */
//include following files for the utilities.
include 'PhpApiSettings.php';
include 'Utilities.php';
include 'Json.php';

///call a function of Utilities.php to generate oAuth token  - with this sample code no OAuth library has been used.
$oauth_result = oAuthTokenGenerator();

//call a function of Utilities.php to verify if there is any error with OAuth token. 
$oauth_moveforward = isFoundOAuthTokenError($oauth_result);


//If IsFoundOAuthTokenError results True, means no error 
//Next is to move forward for the actual request 

if(!$oauth_moveforward){
    //Decode the Raw Json response.
    $json = jsonDecode($oauth_result['temp_json_response']); 
    
    //set Authentication value based on the successful oAuth response.
    //Add a space between 'Bearer' and access _token 
    $oauth_token = sprintf("Bearer %s",$json['access_token']);
 
    // Build the transaction 
    buildTransaction($oauth_token);  
    
}
//end of main script



//function for building a transaction, call to Process Transaction and verify the transaction result.
function buildTransaction($oauth_token){
    // Build the request data
    $request_data = buildRequestData();
    
    //call to a function from Utilities to make the actual request 
    $result = processTransaction($oauth_token, $request_data, URL_VOID_TRANSACTION);
   
    //verify the result and display accoring to the result.
    verifyTransactionResult($result);
}


function buildRequestData(){

    /* you can assign the values from the input source fields instead of hard coded values.
     * Transaction id can be provided from any kind of input source.
     * Transaction id should be one of the Unsettled Transaction (Pending settlement) for void.
     * for Demo purpose, Transaction ID is collected from the input field of the UI form -VoidTransactionJsonUI,
     * Do your own input validation as needed
     */
    
    $transation_id = strval($_POST["TransactionId"]) ;
    //$transation_id = strval(10596845) ; 
    $request_data = array( "transaction_id" => $transation_id );
    
    $request_data = json_encode($request_data);
    
    //optional : Display the Jason response - this may be helpful during initial testing.
    displayRawJsonRequest($request_data);
   
    return $request_data ;  
}


//this function verify the Transaction result by checking the transaction result 
function verifyTransactionResult($trans_result){      

//Handle curl level error, ExitOnCurlError
if($trans_result['curl_error'] ){
    echo "<br>Error occrued : ";
    echo '<br>curl error with Transaction request: ' . $trans_result['curl_error'] ;
    exit();  
}

//If we reach here, we have been able to communicate with the service, 
//next is  to decode the json response 
//Then review Http Status code, response_code and success of the response

$json = jsonDecode($trans_result['temp_json_response']);  

if($trans_result['http_status_code'] != 200){
    if($json['success'] === false){
        echo "<br><br>Transaction Error occurred : "; 
        
        //Optional : display Http status code and message
        displayHttpStatus($trans_result['http_status_code']);
        
        //Optional :to display raw json response
        displayRawJsonResponse($trans_result['temp_json_response']);
       
        echo "<br>Void Transaction  :  failed !";
        //to display individual keys of unsuccessful Transaction Json response
        displayVoidTransactionError($json) ;
    }
    else {
        //In case of some other error occurred, next is to just utilize the http code and message.
        echo "<br><br> Request Error occurred !" ;
        displayHttpStatus($trans_result['http_status_code']);
    }
}
else
{
    //Optional : to display raw json response - this may be helpful with initial testing.
    displayRawJsonResponse($trans_result['temp_json_response']);
   
    //Do your code when Response is available based on the response_code. 
    //Please refer PayTrace-Error page for possible errors and Response Codes
    
    //For transation successfully approved 
    if($json['success']== true && $json['response_code'] == 109){

        echo "<br><br>Void Transaction :  Success !";
        displayHttpStatus($trans_result['http_status_code']);
        //to display individual keys of successful OAuth Json response 
        displayVoidTransactionResponse($json);   
   }
   else{
        //do you code here for any additional verification such as - Avs-response and CSC_response as needed.
        //Please refer PayTrace-Error pagefor possible errors and Response Codes
        //response code = 110 is void unsuccessful. 
        //response code = 114 is for test void transaction.
   }
   
   
}

}

//This function displays void transaction successful response.
function displayVoidTransactionResponse($json_string){
   
    //optional : Display the output
   
    echo "<br><br>Void Transaction Response : ";
    //since php interprets boolean value as 1 for true and 0 for false when accessed.
    echo "<br>success : ";
    echo $json_string['success'] ? 'true' : 'false';  
    echo "<br>response_code : ".$json_string['response_code'] ; 
    echo "<br>status_message : ".$json_string['status_message'] ; 
   
    echo "<br>transaction_id : ".$json_string['transaction_id'] ;  
        
}

//This function displays void transaction error response.
function displayVoidTransactionError($json_string){
    //optional : Display the output
    echo "<br><br> Void Transaction Response : ";
    //since php interprets boolean value as 1 for true and 0 for false when accessed.
    echo "<br>success : ";
    echo $json_string['success'] ? 'true' : 'false';  
    echo "<br>response_code : ".$json_string['response_code'] ; 
    echo "<br>status_message : ".$json_string['status_message'] ;  
   
    //to check the actual API errors and get the individual error keys 
    echo "<br>API Errors : " ;
   
    foreach($json_string['errors'] as $error =>$no_of_errors )
    {
        //Do you code here as an action based on the particular error number 
        //you can access the error key with $error in the loop as shown below.
        echo "<br>". $error;
        // to access the error message in array assosicated with each key.
        foreach($no_of_errors as $item)
        {
           //Optional - error message with each individual error key.
            echo " = " . $item ; 
        } 
    }
    
     
}



?>
    </body>
</html>
