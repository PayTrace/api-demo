<?php
/*
 * This file contains all the common functions that are accessilbe and used on sample codes.
 */
/*
 * This function will find the associated status message from the file 
 * used in Parse_ini_file( path to the file)
 * Returns the status code and message as a string.
 * HttpCodeinfo.ini file contains all the associated message with http status 
 */
function httpStatusInfo($http_status_code){
    $http_message = parse_ini_file(\dirname(__FILE__).'\httpcodeInfo.ini');
    $http_info =  $http_status_code . " " . $http_message[$http_status_code];
    return $http_info ;
}

/*
 * This function will make a request to accuire the OAuth token 
 * Returns an array with Json response, Curl_error and http status code of the request.
 */
function oAuthTokenGenerator(){
   
    // array variable to store the Response value, httpstatus code and curl error.
    $result = array(
                 'temp_json_response' => '',
                 'curl_error' => '',
                 'http_status_code' => '');
    
    // create a new cURL resource
    $ch = curl_init();
    
    //set up oauth_data request 
    $request_data = "grant_type=".GRANT_TYPE."&username=".USERNAME."&password=".PASSWORD ;   

    // set URL and other appropriate options
    curl_setopt($ch, CURLOPT_URL,URL_OAUTH);
    curl_setopt($ch, CURLOPT_POST, true);
    //curl_setopt($ch, CURLOPT_HEADER, true);
    curl_setopt($ch, CURLOPT_HTTP_VERSION, CURL_HTTP_VERSION_1_1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $request_data);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
   
    // following SSL settings should be removed in production code. 
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER,false);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST,0);
    
    //Execute the request
    $response = curl_exec($ch);
    $curl_error = curl_error($ch).' '. curl_errno($ch); 
   
     
    if($response === false){
       
        $result['curl_error'] = $curl_error;
        //To Do : use similar functionality as Finally in C# to free the resources.
        // close cURL resource, and free up system resources
        curl_close($ch);
        
        return $result ; 
    }  
    
    //collect the output data.
    
    /* Following commented code is needed where the header is added with code line above - curl_setopt($ch, CURLOPT_HEADER, true);
       $header_size = curl_getinfo($ch, CURLINFO_HEADER_SIZE);
        $result['json_response'] = substr($response,$header_size);*/
    
    $result['temp_json_response'] = $response ;
    $result['http_status_code'] =  curl_getinfo($ch, CURLINFO_HTTP_CODE);
    
    // close cURL resource, and free up system resources
    curl_close($ch);
    
    return $result;
    
    }
   

// This function will actually execute the transaction based on the request data, url and OAuth token
function processTransaction($oauth_token,$request_data, $url ){
    // array variable to store the Response value, httpstatus code and curl error.
    $result = array(
                 'temp_json_response' => '',
                 'curl_error' => '',
                 'http_status_code' => '');
    
    // create a new cURL resource
    $ch = curl_init();
   
    // set the header
    //$header = array ('Content-type: application/json','Authorization:'.$oauth_token);
    $header[] = 'Content-type: application/json';
    $header[] = 'Authorization:'.$oauth_token;
   
    //echo "<br> Auth token : " .$oauth_token ;
    
    // set URL and other appropriate options for curl to make the request
    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_HTTPHEADER, $header);
   // curl_setopt($ch, CURLOPT_HEADER, true);
    curl_setopt($ch, CURLOPT_HTTP_VERSION, CURL_HTTP_VERSION_1_1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $request_data);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
   
    // following SSL settings should be removed in production code. 
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER,false);
    curl_setopt ($ch, CURLOPT_SSL_VERIFYHOST, 0);
    
    //Execute the request
    $response = curl_exec($ch);
    $curl_error = curl_error($ch).' '. curl_errno($ch); 
    
    //echo "<br>" .$response ; 
    
    if($response === false){
        $result['curl_error'] = $curl_error;
        
        // close cURL resource, and free up system resources
        curl_close($ch);
        
        return $result ; 
    }
    //collect the output data.
    
    /* Following commented code is needed where the header is added with code line above with options - curl_setopt($ch, CURLOPT_HEADER, true);
    * $header_size = curl_getinfo($ch, CURLINFO_HEADER_SIZE);
    $result['json_response'] = substr($response,$header_size);*/
    
    $result['temp_json_response'] = $response ;
    $result['http_status_code'] = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    
    // close cURL resource, and free up system resources
    curl_close($ch);
    
    return $result;
    
  }
  

/*
 * This function will check for the OAuth request error. 
 * Display the error if any and
 * Returns the boolean flag
 */
  
function isFoundOAuthTokenError($oauth_response){
    //set a variable with default 'false' value assuming some error occurred.
    $bool_oauth_error = false ;
    
    //Handle curl level for OAuth error, ExitOnCurlError
    if($oauth_response['curl_error']){
        echo "<br> Error ! ";
        echo '<br>curl error with OAuth request: ' . $oauth_response['curl_error'] ;
        $bool_oauth_error = true ;
        return $bool_oauth_error  ;
    }

    //If we reach here, we have been able to communicate with the service, 
    //next is decode the json response and then review Http Status code of the request 
    //and move forward with further request.
    
    $json = jsonDecode($oauth_response['temp_json_response']);  

    if($oauth_response['http_status_code'] != 200){
        
        $bool_oauth_error = true ;
        echo "<br> OAuth Error ! ";
        
        if(!empty($oauth_response['temp_json_response'])){
   
            // Optional : To display Raw json error response
            displayRawJsonResponse($oauth_response['temp_json_response']);
   
            //Display http status code and message.
            displayHttpStatus($oauth_response['http_status_code']);
   
            //Optional :to display individual keys of unsuccessful OAuth Json response
            displayOAuthError($json) ;
    
        }
        else{
            echo "<br> OAuth Request Error !" ;
             //in case of some other error, utilize the httpstatus code and message.
            displayHttpStatus($oauth_response['http_status_code']);
        }
    }
    else{
        // Reaching at this point means OAuth request was successful. 
        $bool_oauth_error = false ;
    }
    
    return $bool_oauth_error ;
   
}

    
//Tunction to display individual keys of unsuccessful OAuth Json response turns into OAuth error response 
function displayOAuthError($json_string){

    // Display the actual output
    echo "<br><br> OAuth Error :" ;
    echo "<br> error : " . $json_string['error'] ;
    echo "<br> error_description : " . $json_string['error_description'];
    
}

//This function is used to display the http status 
function displayHttpStatus($http_status_code){
    echo "<br><br> Http Status : " . httpStatusInfo($http_status_code);  
}
