<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    Inherits="Home.index" %>

<%@ Register src="IntroduceItems.ascx" tagname="IntroduceItems" tagprefix="uc2" %>

<%@ Register src="TabItems.ascx" tagname="TabItems" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TabItems" runat="Server">
	<uc1:TabItems ID="TabItems1" runat="server" SelectedIndex="1" />
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="Content" runat="Server">
<div class="post">
<h2 style="font-family:Arial;letter-spacing:2px; color:#406090;">云骞开源即时通讯软件</h2>
<p>云骞开源IM是一款使用ASP.NET，Ajax和Comet等技术开发的轻量级IM。主要功能特点如下：</p>

<p>1、云骞开源IM的服务端实际上就是一个ASP.NET网站，因此不需要使用独立服务器，<font color="#ff0000">仅需要一个支持.NET2.0的Web空间即可将IM部署到互联网上。</font></p>
 
<p><strong>源代码下载：</strong><a href="http://luchuncheng.com/blog/article.aspx?ID=12" target="_blank">http://luchuncheng.com/blog/article.aspx?ID=12</a>。</p>

<p>2、可以同时以客户端模式和Web模式运行，两者拥有完全一样的操作界面。</p>

<p>客户端模式：</p>

<p>
<a target="_blank" href="<%= LUCC.Global.BaseUrl %>/posts/res/desktop_pre.jpg">
<img alt="客户端模式" src="<%= LUCC.Global.BaseUrl %>/posts/res/desktop.jpg" />
</a>
</p>
<p>Web模式（兼容IE6,7,8,FireFox,Chrome等主流浏览器）:</p>

<p>
<a target="_blank" href="<%= LUCC.Global.BaseUrl %>/posts/res/webos_pre.jpg">
<img alt="WebOS模式（支持Firefox,Chorme,IE等主流浏览器）" src="<%= LUCC.Global.BaseUrl %>/posts/res/webos.jpg" />
</a>
</p>

<p>3、消息记录（包括消息中的图片和文件）都存储在服务器上，只要可以上网，随时随地都可以浏览您的消息记录。</p>

<p>
<img src="<%= LUCC.Global.BaseUrl %>/posts/res/msg.jpg" />
</p>
<p>&#160;</p>
</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SideMenu" runat="Server">
	<uc2:IntroduceItems ID="IntroduceItems1" runat="server" />
</asp:Content>

