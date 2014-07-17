<%@ Page Title="" Language="C#" MasterPageFile="PostPage.master" AutoEventWireup="true" Inherits="System.Web.UI.Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PostContent" Runat="Server">
<div class='post'>
<h2 style="color:#406090; letter-spacing: 2px; font-family: Arial;">留言管理</h2>
<p><br />
访客打开客服窗口后，即可以发送即时消息同客服实时交流，也可以给客服人员发送留言：<br />
<img alt="检测新留言" src="<%= LUCC.Global.BaseUrl %>/posts/res/10027.jpg" /><br />
客服系统客服端会定时检测是否有新留言，如果有，则在右下角飘窗显示新留言。
<br />
<img alt="检测新留言" src="<%= LUCC.Global.BaseUrl %>/posts/res/10025.jpg" /><br />
点击蓝色的提示文字，可以打开查看阅读未读消息：<br />
<img alt="未读留言窗口" src="<%= LUCC.Global.BaseUrl %>/posts/res/10026.jpg" /><br />
阅读留言时，您可以将任意一个消息标志为重要留言，标志为重要的留言会出现在重要留言页面，以方便以后查看。<br />
<img alt="重要留言" src="<%= LUCC.Global.BaseUrl %>/posts/res/10024.jpg" /><br />
</p>
</div>
</asp:Content>

