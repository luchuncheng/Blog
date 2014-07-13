<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="Home.login" %>


<%@ Register src="TabItems.ascx" tagname="TabItems" tagprefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TabItems" runat="Server">
	<uc1:TabItems ID="TabItems1" runat="server" SelectedIndex="-1" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div class="form">
        <p>
            <h3>
                用户登录</h3>
        </p>
        <p>
            <label for="name">
                用户名</label><br />
            <input class="text" id="name" name="name" value="" type="text" tabindex="2" />
        </p>
        <p>
            <label for="password">
                密码</label><br />
            <input class="text" id="password" name="password" value="" type="password" tabindex="3" />
        </p>
        <p class="no-border">
            <input class="button" type="submit" name="login" value="登 录" tabindex="4" />
        </p>
    </div>
</asp:Content>

