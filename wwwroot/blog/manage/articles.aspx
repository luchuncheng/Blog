<%@ Page Title="" Language="C#" MasterPageFile="BlogManageMasterPage.master" AutoEventWireup="true" Inherits="Home.blog_manage_articles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BlogContent" Runat="Server">
	<script language="javascript" type="text/javascript">
		function Delete(id,title)
		{
			if(confirm(String.format('您确定要删除文章“{0}”？',title)))
			{
				DoCommand('Delete', id);
			}
		}
	</script>		
	<style type="text/css">
		.table
		{
		}
		.table td
		{
			padding:4px;
		}
		.table th
		{
			padding:4px;
		}
		.table .article_title
		{
			width:270px;
		}
		.table .article_renewtime
		{
			width:100px;
		}
		.table .article_state
		{
			width:50px;
		}
		.table .article_istop
		{
			width:40px;
			text-align:center;
		}
		.table .article_operation
		{
			width:70px;
		}
		.table .article_operation a
		{
			margin-right:4px;
		}
	</style>			
	<table class='table'>
		<tr>
			<th class="article_title">文章标题</th>
			<th class="article_renewtime">发表时间</th>
			<th class="article_state">状态</th>
			<th class="article_istop">置顶</th>
			<th class="article_operation">操作</th>
		</tr>
		<%= RenderArticlesTable() %>
	</table>
</asp:Content>

