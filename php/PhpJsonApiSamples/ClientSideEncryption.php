<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title>Client side Encryption</title>
         <!-- This is the PayTrace End-to-End Encryption library: -->
        <script src="https://rmapi.paytrace.com/assets/e2ee/paytrace-e2ee.js">
            </script>

       
    </head>
    <body>
        <script>
            // this hooks form submission.
            $(document).ready(function() {
            // do this first, or wrap in a try/catch to ensure the form is never un-hooked
            paytrace.hookFormSubmit('#DemoForm');
            // set the key from an AJAX call (in this case via a relative URL)
            paytrace.setKeyAjax('DemoPublic_Key.pem');
        });
        </script>
            <form id="DemoForm" action="ClientEncryptionDemo.php" method="post">
        <div>
    	    <!-- On each field to be encrypted at the browser, add the pt-encrypt class. -->
            <p>Enter Credit Card Number : <input id="ccNumber" type="text" class="form-control pt-encrypt"  name="ccNumber" placeholder="Credit card number"/> </p>
            <br>
            <p>Enter Credit Card Number : <input id="ccCSC" type="text" class="form-control pt-encrypt"  name="ccCSC" placeholder="Card security code" /> </p>	
            <br>
            <input type="submit" id="enterPayment" value="Submit" name="commit" /> 
    	</div>
        </form>
        <br>
            <a href="Default.php">Back to Home </a> 
        <br>
    </body>
</html>
