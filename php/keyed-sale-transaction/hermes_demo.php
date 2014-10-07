<?php

require 'vendor/autoload.php';

$client = new GuzzleHttp\Client();
$res = $client->post('http://localhost:3000/oauth/token', [
  'body' => [
    'grant_type' => 'password', 
    'username' => 'demo123', 
    'password' => 'demo123'
    ]
  ]);
$json = $res->json();
// var_export($json);             // Outputs the JSON decoded data
$token = $json['access_token'];

?>