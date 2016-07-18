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
 *This page will show you php sample for the swiped sale.
 */
//include following files for the utilities.
include 'PhpApiSettings.php';
include 'Utilities.php';
include 'Json.php';

//call a function of Utilities.php to generate oAuth token 
//This sample code doesn't use any 0Auth Library
$oauth_result = oAuthTokenGenerator();

//call a function of Utilities.php to verify if there is any error with OAuth token. 
$oauth_moveforward = isFoundOAuthTokenError($oauth_result);


//If IsFoundOAuthTokenError results True, means no error 
//next is to move forward for the actual request 

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

function buildTransaction($oauth_token){
    // Build the request data
    $request_data = buildRequestData();
    
    //call to make the actual request
    $result = processTransaction($oauth_token,$request_data, URL_SWIPED_SALE );
    
    /*echo "<br>json_response : " . $result['json_response'];
    echo "<BR>curl_error : ".$result['curl_error'];
    echo "<br>http_status_code :".  $result['http_status_code'];
    */

    //check the result
    verifyTransactionResult($result);
}


function buildRequestData(){

    /* you can assign the values from the input source fields instead of hard coded values.
     * Note: Few request parameters are optional and few are required. Those depends on your virtual Terminal Account security settings. 
     * Swipe value can be detected from the Magnetic stripe reader(Credit cared reader Device)
     * Swipe value will include both 'Track1' and 'Track2' data.
     * If PaytTrace client side encryption is used, use 'encrypted_swipe' as key name instead of 'swipe' in the request.
     */
    $request_data = array(
                    "amount" => "4.50",
                    "swipe" => "%B4012881888818888^Demo/Customer^2412101001020001000000701000000?;4012881888818888=24121010010270100001?",
                    "csc"=> "999",
                    "billing_address"=> array(
                        "name"=> "Tom Harper",
                        "street_address"=> "8320 E. West St.",
                        "city" => "Spokane",
                        "state"=> "WA",
                        "zip"=> "85284")
                         );
    
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
        echo "<br><br>Transaction Error occurred : "; 
        
        //Optional : display Http status code and message
        displayHttpStatus($trans_result['http_status_code']);
        
        // Optional :to display raw json response
        displayRawJsonResponse($trans_result['temp_json_response']);
       
        echo "<br>Swiped sale :  failed !";
        //to display individual keys of unsuccessful Transaction Json response
        displaySwipedSaleTransactionError($json) ;
    }
    else {
        //in case of  some other error occurred, next is to just utilize the http code and message.
        echo "<br><br> Request Error occurred !" ;
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
    if($json['success']== true && $json['response_code'] == 101){

        echo "<br><br>Swiped sale :  Success !";
        displayHttpStatus($trans_result['http_status_code']);
        //to display individual keys of successful OAuth Json response 
        displaySwipedSaleTransactionResponse($json);   
   }
   else{
        //do you code here for any additional verification such as - Avs-response and CSC_response as needed.
        //Please refer PayTrace-Error pagefor possible errors and Response Codes
        //success = true and response_code == 103 approved but voided because of CSC did not match.
   }
}
  
}


//This function displays Swiped transaction successful response.
function displaySwipedSaleTransactionResponse($json_string){
   
    //optional : Display the output
   
    echo "<br><br> Swiped Sale Response : ";
    //since php interprets boolean value as 1 for true and 0 for false when accessed.
    echo "<br>success : ";
    echo $json_string['success'] ? 'true' : 'false';  
    echo "<br>response_code : ".$json_string['response_code'] ; 
    echo "<br>status_message : ".$json_string['status_message'] ; 
    echo "<br>transaction_id : ".$json_string['transaction_id'] ;  
    echo "<br>approval_code : ".$json_string['approval_code'] ;
    echo "<br>approval_message : ".$json_string['approval_message'] ;
    echo "<br>avs_response : ".$json_string['avs_response'] ;
    echo "<br>csc_response : ".$json_string['csc_response'] ; 
    echo "<br>external_transaction_id: ".$json_string['external_transaction_id'] ;
         
}


//This function displays swiped transaction error response.
function displaySwipedSaleTransactionError($json_string){
    //optional : Display the output
    echo "<br><br> Swiped Sale Response : ";
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
            echo "  " . $item ; 
        } 
    }
    
     
}



?>
    
    </body>
</html>
