<%@ Page Title="" Language="C#" MasterPageFile="BlogManageMasterPage.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Home.blog_edit" %>

<%-- 在此处添加内容控件 --%>

<asp:Content ID="Content4" ContentPlaceHolderID="BlogContent" Runat="Server">

	<script type="text/javascript" charset="utf-8" src="../../KindEditor/kindeditor.js"></script>
<script type="text/javascript" language="javascript">
	KE.show({
		id : 'article_content',
        imageUploadJson : '<%= LUCC.Global.BaseUrl %>/upload_json.ashx',
        fileManagerJson : '<%= LUCC.Global.BaseUrl %>/file_manager_json.ashx',
		resizeMode : 1
	});
	function onpost()
	{
		if(document.getElementById("article_title").value == "")
		{
			alert("请输入文章标题！");
			document.getElementById("article_title").focus();
			return false;
		}
		if(document.getElementById("article_summary").value == "")
		{
			alert("请输入文章摘要！");
			document.getElementById("article_summary").focus();
			return false;
		}
		return true;
	}
</script>		
<style type="text/css">
	#article_info
	{
		width: 580px;
	}
	
	#article_info td
	{
		padding: 5px 0px;
		color: Black;
		vertical-align: top;
		font-family: Courier New;
		font-size: 12px;
	}
	#article_info #article_istop
	{
		margin:0px;
		padding:0px;
	}
	#article_info label
	{
		padding:0px 4px 0px 4px;
	}
	.kindeditor textarea
	{
		border-width:0px;
	}
</style>	
<table id="article_info">
	<tr>
		<td>标题：</td>
		<td><input class="text" name="article_title" id="article_title" type="text" style="width: 520px" value="<%= (_articleInfo == null ? "" : _articleInfo["Title"].ToString())%>" /></td>
	</tr>
	<tr>
		<td style="vertical-align:top;">摘要：</td>
		<td><textarea class="text" name="article_summary" id="article_summary" style="width: 520px; height: 97px;"><%= (_articleInfo == null ? "" : _articleInfo["Summary"].ToString())%></textarea></td>
	</tr>
	<tr>
		<td>分类：</td>
		<td>
			<select name="atricle_catetory" id="atricle_catetory" size="1" style="width: 234px">
				<%= RenderSelectItems()%>
			</select>
		</td>
	</tr>
	<tr>
		<td style="vertical-align:middle;">选项：</td>
		<td>
			&nbsp;<input id="article_istop" name="article_istop" <%= (_articleInfo == null || Convert.ToInt64(_articleInfo["IsTop"]) != 1 ? "" : "CHECKED")%> type="checkbox"/><label for="article_istop">置顶</label>
		</td>
	</tr>
</table>
<br />
<div class='kindeditor'>	
<textarea id="article_content" name="article_content" style="width:580px;height:500px;"><%= (_articleInfo == null ? "" : _articleInfo["Content"].ToString()).Replace("&","&amp;")%></textarea>
</div>
<br />
<div style=" width:100%; text-align:right;">
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
	<input class="button" type="submit" name="save" value="保存草稿" tabindex="4" onclick="return onpost()" />
</asp:PlaceHolder>
<input class="button" type="submit" name="post" value="<%= _articleInfo == null || Convert.ToInt64(_articleInfo["IsPost"]) == 0 ? "发 布" : "更 新" %>" tabindex="4" onclick="return onpost()" />
</div>
</asp:Content>
