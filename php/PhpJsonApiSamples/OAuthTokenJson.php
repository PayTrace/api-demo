<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title>OAuth Token</title>
    </head>
    <body>
     <br>
	<a href="Default.html">Back to Home </a> 
     <br>
 <?php
       
/* This code will shows how to access and make a request of OAuth token  */

include 'PhpApiSettings.php';
include 'Utilities.php';
include 'Json.php';


//call a function of Utilities.php to generate oAuth toaken 
$oauth_result = oAuthTokenGenerator();

//Handle curl level error, ExitOnCurlError
if($oauth_result['curl_error'] )
{
        echo "<br>Error : ";
        echo '<br>curl error with OAuth request: ' . $oauth_result['curl_error'] ;
        exit();  
}

//If we reach here, we have been able to communicate with the service, 
//next is decode the json response and then review Http Status code of the request
 
$json = jsonDecode($oauth_result['temp_json_response']);  

if($oauth_result['http_status_code'] != 200){
    echo "<br>Error : ";
    if(!(is_null($oauth_result['temp_json_response'])) ){
        // to display json response
        displayRawJsonResponse($oauth_result['temp_json_response']);
   
        //Display http status code and message.
        displayHttpStatus($oauth_result['http_status_code']);
   
        //to display individual keys of unsuccessful OAuth Json response
        displayOAuthError($json) ;
    }
 else {
        //incase of any other error , utilize the http status code and message.
        //Optional : Display http status code and message.
        echo "<br>" . "Request error occurred !";
        displayHttpStatus($oauth_result['http_status_code']);
    }
}
else{
    
     echo "<br>" . "OAuth Request: Success !";
    //To display json response
    displayRawJsonResponse($oauth_result['temp_json_response']);
   
    //Display http status code and message.
    displayHttpStatus($oauth_result['http_status_code']);
   
    // to display individual keys of successful OAuth Json response 
    displayOAuth($json, $oauth_result['http_status_code']);
}

//function to display the individual keys of successful OAuth Json response 
function displayOAuth($json_string){
   
    //Display the output
    echo "<br><br> OAuth Response : ";
    echo "<br>access_token : ".$json_string['access_token'] ; 
    echo "<br>token_type : ".$json_string['token_type'] ; 
    echo "<br>expires_in : ".$json_string['expires_in'] ;       
}


 ?>
</body>
</html>
