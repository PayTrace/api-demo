<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientSideEncryptionDemo.aspx.cs" Inherits="CSharpDotNetJsonSample.ClientSideEncryptionDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

     <title>Client Encryption Demo </title>
     <!-- This is the PayTrace End-to-End Encryption library: -->
     <script src="https://api.paytrace.com/assets/e2ee/paytrace-e2ee.js" >
	 </script>

</head>

<body>
     <script>
         $(document).ready(function () {
         	paytrace.hookFormSubmit('#enterPayment1');
         	paytrace.setKeyAjax('DemoPublic_Key.pem');
         });// set the key from an AJAX call (in this case via a relative URL of the public key file)
     </script>

    <form id="enterPayment1" runat="server" method ="post" >
        
    	<div>
    	    <!-- On each field to be encrypted at the browser, add the pt-encrypt class. -->
    		<input id="ccNumber" type="text" class="form-control pt-encrypt"  name="ccNumber" placeholder="Credit card number"/>
    		<br />
    		<input id="ccCSC" type="text" class="form-control pt-encrypt"  name="ccCSC" placeholder="Card security code" />
        	<br/>
            <input type="submit" id="enterPayment" value="Submit" name="commit" />
           
    	</div>

    </form>
    <br/>
	<a href="Default.aspx">Back to Home </a> 
	<br/>
</body>

</html>