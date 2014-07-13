<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="Home.download" %>


<%@ Register src="TabItems.ascx" tagname="TabItems" tagprefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TabItems" runat="Server">
	<uc1:TabItems ID="TabItems1" runat="server" SelectedIndex="4" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
</asp:Content>

