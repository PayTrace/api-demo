<?php

/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/*
 * This file contains all the funcation related to JSON data.
 */

//function to decode the Json response.
function jsonDecode($json_string){
  //Decode the Json Response.
  if(empty($json_string)){
        return null ;
  }
  else{
      $json = json_decode($json_string, true); 
  }
  return $json ;
}

//function to encode the string into Json
function jsonEncode($json_string){
    //Encode the Json Response.
    $json = json_encode($json_string);   
    return $json ;
}

// this function will display the json string 
function displayRawJsonResponse($json_string){
    echo "<br><br> Raw Json Response : " . $json_string ;
}

// this function will display the json string 
function displayRawJsonRequest($json_string){
    echo "<br> Raw Json Request : " . $json_string ;
}