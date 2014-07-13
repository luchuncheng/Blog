<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Decompress.aspx.cs" Inherits="DecompressPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件解压缩(ZIP)</title>
	<link href="css/table.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 50px;
        }
        .style2
        {
            width: 512px;
        }
        .style3
        {
            color: #FF0000;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
	<div class="table_blue">
		<table cellpadding="0" cellspacing="0">
			<tr class="header">
				<td colspan="3">文件解压缩(ZIP)</td>
			</tr>
			<tr>
				<td class="style1">源文件：</td>
				<td class="style2"><asp:TextBox ID="TextBox1" runat="server" Width="500px" ></asp:TextBox></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">解压到：</td>
				<td class="style2"><asp:TextBox ID="TextBox2" runat="server" Width="500px" ></asp:TextBox></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">&nbsp;</td>
				<td class="style2">
					<asp:CheckBox ID="CheckBox1" runat="server" Text="启动新线程" />
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">&nbsp;</td>
				<td style="text-align:right;" class="style2"><asp:Button ID="btnUpdate" 
						runat="server" Text="解&nbsp;&nbsp;压" Width="79px" onclick="btnUpdate_Click"/></td>
				<td>&nbsp;</td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
