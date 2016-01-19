<%@ Page Language="C#" Inherits="AspNetClientEncryptionExample.AuthenticationTokenTest" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>AuthenticationTokenTest</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:Button id="BtnAuth" runat="server" Text="Click to get Authetication Token!" OnClick="BtnAuthClicked" />
	
	</form>
	<br>
	<a href="http://127.0.0.1:8080/Default.aspx">back to Home </a> 
	<br>
</body>
</html>

