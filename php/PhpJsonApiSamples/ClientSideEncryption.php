<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title>Client Side Encryption</title>
    </head>
    <body>
        <br>
            <a href="Default.html">Back to Home </a>  
        <br>
 <?php
    // Access the encyrpted request fields and store the Encrypted Value 
    // Send those values in the API request by prepending KeyName with 'encrypted_'.
    $encrypted_number = $_POST['ccNumber'];
    $encrypted_csc =$_POST['ccCSC'] ;
        
    // Optional : display the Encrypted value.
    echo "<br>encrypted_ccNumber :" .$encrypted_number ;
    echo "<br><br>encrypted_csc :" .$encrypted_csc ;
        
        
  ?>
    </body>
</html>
