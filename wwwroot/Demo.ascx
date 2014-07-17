<%@ Control Language="C#" AutoEventWireup="true" Inherits="Demo" %>
<script language="javascript" type="text/javascript">
function Lesktop_StartChat(name)
{
	StartService(
		function()
		{
			var curuser = Core.Session.GetUserName();
			if(curuser != null && curuser.toLowerCase() != name.toLowerCase())
			{
				Core.Session.GetGlobal("ChatService").Open(name, false);
			}
		}
	);
}
</script>

<div class="sidemenu" id="sidemenu_demo" style="display:none;">	
	<h3 style="font-family:Arial;">在线演示</h3>
	<ul>				
		<li><a href="http://s.eim.cc" target="_blank" title="Web模式演示">Web模式演示</a></li>
		<li><a href="http://s.eim.cc/2.0.0.15/Client.zip" title="桌面版下载">桌面版下载</a></li>
	</ul>	
</div>
