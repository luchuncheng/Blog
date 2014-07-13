<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="Home.about" %>

<%@ Register src="IntroduceItems.ascx" tagname="IntroduceItems" tagprefix="uc1" %>

<%@ Register src="TabItems.ascx" tagname="TabItems" tagprefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TabItems" runat="Server">
	<uc1:TabItems ID="TabItems1" runat="server" SelectedIndex="5" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" Runat="Server">
	<style type="text/css">
		.style1
		{
			width: 355px;
		}
		.style2
		{
			width: 82px;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">						
	<table class='table'>
		<tr>
			<td class="style2">联系人：</td>
			<td class="style1">卢春城</td>
		</tr>
		<tr>
			<td class="style2">电子邮件：</td>
			<td class="style1"><a href="mailto:mrlucc@126.com">mrlucc@126.com</a></td>
		</tr>
		<tr>
			<td class="style2">QQ讨论群：</td>
			<td class="style1">25160257</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SideMenu" Runat="Server">
	<uc1:IntroduceItems ID="IntroduceItems1" runat="server" />
</asp:Content>

