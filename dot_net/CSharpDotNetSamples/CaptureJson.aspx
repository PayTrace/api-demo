<%@ Page Language="C#" Inherits="AspNetClientEncryptionExample.CaptureJson" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>CaptureJson</title>
</head>
<body>
	<form id="form1" runat="server">
		<br>
			<input id="TransactionNumber" type="text" name="TransactionNumber" placeholder="Pre Authorizied Transaction Number"/>
    		<br>
    		<br>
         	<asp:Button id="BtnCapture" runat="server" Text="Capture the Authorized Transaction  " OnClick="BtnCaptureClicked" ToolTip="Capture the Authorizied Transaction" />
	</form>
	<br>
	<a href="http://127.0.0.1:8080/Default.aspx">Back to Home </a> 
	<br>
</body>
</html>

