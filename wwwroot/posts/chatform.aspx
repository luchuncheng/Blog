<%@ Page Title="" Language="C#" MasterPageFile="PostPage.master" AutoEventWireup="true" Inherits="MyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PostContent" Runat="Server">
<div class="post">
<h2 style="font-family:Arial;letter-spacing:2px; color:#406090;">聊天窗口（支持群聊和私聊）</h2>
<h3>发送消息（可发送文字，图片和表情）</h3>
<p>
<img alt="发送消息（可发送文字，图片和表情）" src="<%= LUCC.Global.BaseUrl %>/posts/res/10001.jpg" />
<img alt="发送消息（可发送文字，图片和表情）" src="<%= LUCC.Global.BaseUrl %>/posts/res/10002.jpg" />
<img alt="发送消息（群聊）" src="<%= LUCC.Global.BaseUrl %>/posts/res/10010.jpg" />
</p>
<hr />
<h3>截图（桌面模式下）</h3>
<p>
<img alt="截图（桌面模式下）" src="<%= LUCC.Global.BaseUrl %>/posts/res/10003.jpg" />
</p>
<hr />
<h3>消息历史</h3>
<p>
<img alt="消息历史" src="<%= LUCC.Global.BaseUrl %>/posts/res/10009.jpg" />
</p>
</div>
</asp:Content>

