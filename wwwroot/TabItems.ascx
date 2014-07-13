<%@ Control Language="C#" AutoEventWireup="true" Inherits="Home.TabItems" %>

<li class="first" id="<%= SelectedIndex == 1 ? "current" : "" %>"><a href="<%= AppPath %>/Default.aspx">首页</a></li>
<li id="<%= SelectedIndex == 2 ? "current" : "" %>"><a href="<%= AppPath %>/posts/chatform.aspx">功能介绍</a></li>
<li id="<%= SelectedIndex == 4 ? "current" : "" %>"><a href="<%= AppPath %>/blog/article.aspx?ID=12">下载</a></li>
<li id="<%= SelectedIndex == 3 ? "current" : "" %>"><a href="<%= AppPath %>/blog/index.aspx">博客</a></li>
<%--<li id="<%= SelectedIndex == 8 ? "current" : "" %>"><a href="<%= AppPath %>/forum/view.aspx?Forum=200">论坛</a></li>--%>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
<li id="<%= SelectedIndex == 7 ? "current" : "" %>""><a href="<%= AppPath %>/blog/manage/articles.aspx">博客管理</a></li>
</asp:PlaceHolder>
<li id="<%= SelectedIndex == 5 ? "current" : "" %>"><a href="<%= AppPath %>/about.aspx">联系方式</a></li>
