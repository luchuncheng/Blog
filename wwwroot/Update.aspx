<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="Update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    	<asp:FileUpload ID="FileUpload1" runat="server" Width="374px" />
		<br />
		<br />
		<asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" Text="更新" 
			Width="79px" />
    
    </div>
    </form>
</body>
</html>
