<%@ Page Language="C#" Inherits="AspNetClientEncryptionExample.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<!---<asp:Button id="button1" runat="server" Text="Click me!" OnClick="button1Clicked" /> -->
		<br>
		<a href="http://127.0.0.1:8080/clientsideencryption.aspx">ClientSide Encryption </a> 
		<br>
		<a href="http://127.0.0.1:8080/AuthenticationTokenTest.aspx">Authentication Token</a> 
		<br>
		<a href="http://127.0.0.1:8080/KeyedSaleJson.aspx">Keyed Sale (Card not present)</a> 
		<br>
		<a href="http://127.0.0.1:8080/SwipedSaleJson.aspx">Swiped Sale (Card present)</a> 
		<br>
		<a href="http://127.0.0.1:8080/KeyedRefundJson.aspx">Keyed Refund </a> 
		<br>
		<a href="http://127.0.0.1:8080/VoidTransactionJson.aspx">Void Transaction</a> 
		<br>
		<a href="http://127.0.0.1:8080/CreateCustomerJson.aspx">Create Customer Profile</a> 
		<br>
	</form>
</body>
</html>

