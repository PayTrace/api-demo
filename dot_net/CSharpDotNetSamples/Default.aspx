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
			<td><a href="ClientSideEncryptionDemo.aspx">ClientSide Encryption </a> </td>
		</tr>   
		<tr>
    		<td><a href="AuthenticationTokenTest.aspx">Authentication Token</a> </td>
  		</tr>
		<tr>
			<td><a href="KeyedSaleJson.aspx">Keyed Sale (Card not present)</a> </td>
  		</tr>
		<tr>
			<td><a href="SwipedSaleJson.aspx">Swiped Sale (Card present)</a> </td>
 		 </tr>
		<tr>
			<td><a href="KeyedAuthorizationJson.aspx">Keyed Authorization(Card-Not Present)</a> </td>
  		</tr>
		<tr>
			<td><a href="CaptureJson.aspx">Capture Authorized Transaction</a> </td>
  		</tr>
		<tr>
			<td><a href="KeyedRefundJson.aspx">Keyed Refund</a> </td>
 		 </tr>
		<tr>
			<td><a href="VoidTransactionJson.aspx">Void Transaction</a> </td>
  		</tr>
		<tr>
			<td><a href="CreateCustomerJson.aspx">Create Customer Profile</a> </td>
 		</tr>
 		<tr>
    		<td><a href="VaultSaleByCustomerIDJson.aspx">Vault Sale By Customer ID</a> </td>
  		</tr>
	
</table>
	</form>
</body>
</html>

