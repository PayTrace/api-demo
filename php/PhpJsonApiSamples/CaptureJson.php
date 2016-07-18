<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title>Capture a Transaction</title>
    </head>
    <body>
        <br>
            <a href="Default.html">Back to Home </a> 
        <br>
<?php
/*
 *This page will show you php sample for the Capture sale.
 */


//include following files for the utilities.
include 'PhpApiSettings.php';
include 'Utilities.php';
include 'Json.php';

//call a function of Utilities.php to generate oAuth token  - with this sample code no OAuth library has been used.
$oauth_result = oAuthTokenGenerator();

//call a function of Utilities.php to verify if there is any error with OAuth token. 
$oauth_moveforward = isFoundOAuthTokenError($oauth_result);

//If IsFoundOAuthTokenError results True, means no error 
//Next is to move forward for the actual request and utilize the OAuth token

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
    $result = processTransaction($oauth_token, $request_data, URL_CAPTURE);

    //verify the result and display according to the result.
    verifyTransactionResult($result);
}


function buildRequestData(){

    /* you can assign the values from the input source fields instead of hard coded values.
     * Note: amount is optional if not provided, transaction will be catured with the authorized amount 
     * Transaction id can be provided from any kind of input source,
     * for Demo purpose, Transaction ID is collected from the input field of the UI form - CaptureJsonUI,
     * Do your own input validation.
     */
    
    $transation_id = $_POST["TransactionId"] ;
    
    $request_data = array( "transaction_id" => $transation_id );
    
    // encode Json data by calling a function from json.php
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
//next is decode the json response and then review Http Status code, response_code and success of the response

$json = jsonDecode($trans_result['temp_json_response']);  

if($trans_result['http_status_code'] != 200){
    if($json['success'] === false){
        echo "<br><br>Transaction Error occured : "; 
        
        //Optional : display Http status code and message
        displayHttpStatus($trans_result['http_status_code']);
        
        // Optional :to display raw json response
        displayRawJsonResponse($trans_result['temp_json_response']);
       
        echo "<br>Capture a transaction :  failed !";
        //to display individual keys of unsuccessful Transaction Json response
        displayCaptureTransactionError($json) ;
    }
    else {
        //in case of  some other error occured, next is to just utilize the http code and message.
        echo "<br><br> Request Error occured !" ;
        displayHttpStatus($trans_result['http_status_code']);
    }
}
else
{
    // Optional : to display raw json response - this may be helpful with initial testing.
    displayRawJsonResponse($trans_result['temp_json_response']);
   
    // Do your code when Response is available based on the response_code. 
    // Please refer PayTrace-Error page for possible errors and Response Codes
    
    // For transation successfully approved 
    if($json['success']== true && $json['response_code'] == 112){

        echo "<br><br>Capture a Transaction :  Success !";
        displayHttpStatus($trans_result['http_status_code']);
        //to display individual keys of successful OAuth Json response 
        displayCaptureTransactionResponse($json);   
   }
   else{
        //Do you code here for any additional verification such as - Avs-response and CSC_response as needed.
        //Please refer PayTrace-Error page for possible errors and Response Codes
       
   }
}
  
}


//This function displays Capture transaction successful response.
function displayCaptureTransactionResponse($json_string){
   
    //optional : Display the output
   
    echo "<br><br> Capture Transaction Response : ";
    //since php interprets boolean value as 1 for true and 0 for false when accessed.
    echo "<br>success : ";
    echo $json_string['success'] ? 'true' : 'false';  
    echo "<br>response_code : ".$json_string['response_code'] ; 
    echo "<br>status_message : ".$json_string['status_message'] ; 
   
    echo "<br>transaction_id : ".$json_string['transaction_id'] ;  
    
    echo "<br>external_transaction_id: ".$json_string['external_transaction_id'] ;
         
}


//This function displays Capture transaction error response.
function displayCaptureTransactionError($json_string){
    //optional : Display the output
    echo "<br><br> Capture Transaction Response : ";
    //since php interprets boolean value as 1 for true and 0 for false when accessed.
    echo "<br>success : ";
    echo $json_string['success'] ? 'true' : 'false';  
    echo "<br>response_code : ".$json_string['response_code'] ; 
    echo "<br>status_message : ".$json_string['status_message'] ;  
    echo "<br>external_transaction_id: ".$json_string['external_transaction_id'] ;  
   
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
