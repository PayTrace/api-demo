<%@ Page Language="C#" Inherits="AspNetClientEncryptionExample.TransactionVoidJson" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>TransactionVoidJson</title>
</head>
<body>
	<form id="form1" runat="server">
	<asp:Button id="BtnTransactionVoid" runat="server" Text="Void Transaction!" OnClick="BtnTransactionVoidClicked" />
	</form>
	<br>
	<a href="http://127.0.0.1:8080/Default.aspx">Back to Home </a> 
	<br>
</body>
</html>

