<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="Home._case" %>

<%@ Register src="IntroduceItems.ascx" tagname="IntroduceItems" tagprefix="uc1" %>

<%@ Register src="TabItems.ascx" tagname="TabItems" tagprefix="uc1" %>

<asp:Content ID="Content5" ContentPlaceHolderID="TabItems" runat="Server">
	<uc1:TabItems ID="TabItems1" runat="server" SelectedIndex="6" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
<img class="case_img" alt="辽宁福鞍集团" src="<%= LUCC.Global.BaseUrl %>/posts/res/lnfa.gif" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SideMenu" Runat="Server">
	<uc1:IntroduceItems ID="IntroduceItems1" runat="server" />
</asp:Content>

