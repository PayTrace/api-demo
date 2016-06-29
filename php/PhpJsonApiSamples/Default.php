<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title>PHP Json samples</title>
	<style>
		table, th 
		{
    		border: 1px solid black;
   			border-collapse: collapse;
		}
		th, td 
		{
		    padding: 5px;
		}
		th
		{
    		background-color: #0066CC;
    		color: white;
		}
		tr:nth-child(even){background-color: #f2f2f2}
	</style>
    </head>
    
    <body>
	<form id="form1" runat="server">
	<table style="width:40%">
		<tr><th>Select any option </th></tr>
		<tr> 
                        <!-- Demo1 checks the client side validations and then submit the valid form with PayTrace client side library  -->
			<td><a href="ClientSideEncryptionDemo1.html">Client Side Encryption Demo 1 </a> </td>
		</tr>   
                <tr> 
			 <!-- Demo2 submits the form with PayTrace client side library as soon as form is submitted. -->
                        <td><a href="ClientSideEncryptionDemo2.html">Client Side Encryption Demo 2 </a> </td>
		</tr> 
		<tr>
                    <td><a href="OAuthTokenJson.php">Authentication Token</a> </td>
  		</tr>
		<tr>
			<td><a href="KeyedSaleJson.php">Keyed Sale (Card-Not Present)</a> </td>
  		</tr>
		<tr>
			<td><a href="SwipedSaleJson.php">Swiped Sale (Card Present)</a> </td>
 		 </tr>
		<tr>
			<td><a href="KeyedAuthorizationJson.php">Keyed Authorization(Card-Not Present)</a> </td>
  		</tr>
		<tr>
			<td><a href="CaptureJsonUI.php">Capture Authorized Transaction</a> </td>
  		</tr>
		<tr>
			<td><a href="KeyedRefundJson.php">Keyed Refund</a> </td>
 		 </tr>
		<tr>
			<td><a href="VoidTransactionJsonUI.php">Void Transaction</a> </td>
  		</tr>
		<tr>
			<td><a href="CreateCustomerJson.php">Create Customer Profile (Tokenization)</a> </td>
 		</tr> 
 		<tr>
    		<td><a href="VaultSaleByCustomerIDJson.php">Vault Sale By Customer ID</a> </td>
  		</tr>
	
</table>
	</form>

    </body>
</html>
