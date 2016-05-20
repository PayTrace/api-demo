<?php
// This file holds all the settings related to API.
/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
//Define variables that holds API settings and urls.
//Get the user credential for the account and change the user credentials
define("USERNAME", "TestSampleApi");
define("PASSWORD", "TestS@mple123"); //pvc
define("GRANT_TYPE","password");

define("BASE_URL","https://rmapi.paytrace.com"); //PVC
//define("BASE_URL","https://api.paytrace.com"); //Production

//API version
define("API_VERSION", "/v1");

// Url for OAuth Token 
define("URL_OAUTH",BASE_URL."/oauth/token");

// URL for Keyed Sale
define("URL_KEYED_SALE",BASE_URL.API_VERSION."/transactions/sale/keyed");

// URL for Swiped Sale
define("URL_SWIPED_SALE" , BASE_URL.API_VERSION."/transactions/sale/swiped");
		
// URL for Keyed Authorization
define("URL_KEYED_AUTHORIZATION" ,BASE_URL.API_VERSION."/transactions/authorization/keyed");

// URL for Keyed Refund
define("url_keyed_refund" , BASE_URL.API_VERSION."/transactions/refund/keyed");

// URL for Capture Transaction
define("url_capture", BASE_URL.API_VERSION."/transactions/authorization/capture");

// URL for Void Sale Transaction
define("url_void_transaction", BASE_URL. API_VERSION."/transactions/void");

// URL for Vault Sale by CustomerId Method
define("url_create_customer", BASE_URL.$API_VERSION."/customer/create");
		
// URL for Vault Sale by CustomerId Method
define("url_vault_saleby_customer_id", BASE_URL.API_VERSION."/transactions/sale/by_customer");








//URL for API Method
$ping_url =  API_VERSION."/ping"; 

