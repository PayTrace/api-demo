<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<!-- this page is providing UI to capture a Transaction number and 
triggers 'CaptureJson.php' which is a sample code for capturing a transaction. -->

<html>
    <head>
        <meta charset="UTF-8">
        <title>Capture a Transaction </title>
    </head>
    <body>
        <form action="CaptureJson.php" method="post">
            <p>Authorized Transaction Id : <input type="text" name="TransactionId" /></p>
            <p><input type="submit" value="Capture Transaction" /></p>
        </form>
        <br>
            <a href="Default.html">Back to Home </a> 
        <br>
    </body>
</html>
