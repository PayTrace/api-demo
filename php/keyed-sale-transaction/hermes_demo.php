<?php

// use the Guzzle HTTP client library
// see: http://guzzle.readthedocs.org/en/latest/index.html
require 'vendor/autoload.php';
$client = new GuzzleHttp\Client();

// DRY
$auth_server = 'http://localhost:3000';

// send a request to the authentication server
// note: normally, you'd store the username/password in a more secure fashion!
$res = $client->post("$auth_server/oauth/token", [
  'body' => [
    'grant_type' => 'password', 
    'username' => 'demo123', 
    'password' => 'demo123'
    ]
  ]);

// grab the results (as JSON)
$json = $res->json();

// get the access token
$token = $json['access_token'];

// create a fake sale transaction
$sale_data = [
  'amount' => 19.99,
  'credit_card' => [
    'number' => '4111111111111111',
    'expiration_month' => 12,
    'expiration_year' => 20
  ]
];

// post the transaction to hermes
$res = $client->post("$auth_server/v1/transactions/sale/keyed", [
  'headers' => ['Authorization' => "Bearer $token"],
  'json' => $sale_data
  ]);

// dump the json results
var_export($res->json());

?>