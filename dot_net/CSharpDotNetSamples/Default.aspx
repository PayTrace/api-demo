<%@ Page Language="C#" Inherits="AspNetClientEncryptionExample.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
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
			<td><a href="http://127.0.0.1:8080/clientsideencryption.aspx">ClientSide Encryption </a> </td>
		</tr>   
		<tr>
    		<td><a href="http://127.0.0.1:8080/AuthenticationTokenTest.aspx">Authentication Token</a> </td>
  		</tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/KeyedSaleJson.aspx">Keyed Sale (Card not present)</a> </td>
  		</tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/SwipedSaleJson.aspx">Swiped Sale (Card present)</a> </td>
 		 </tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/KeyedAuthorizationJson.aspx">Keyed Authorization(Card-Not Present)</a> </td>
  		</tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/CaptureJson.aspx">Capture Authorized Transaction</a> </td>
  		</tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/KeyedRefundJson.aspx">Keyed Refund</a> </td>
 		 </tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/VoidTransactionJson.aspx">Void Transaction</a> </td>
  		</tr>
		<tr>
			<td><a href="http://127.0.0.1:8080/CreateCustomerJson.aspx">Create Customer Profile</a> </td>
 		</tr>
	
</table>
	</form>
</body>
</html>

