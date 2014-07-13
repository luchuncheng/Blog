using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using LUCC;
using Core;

namespace Home
{
	public partial class blog_manage_articles : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Home.CommandCtrl commandCtrl = Master.Master.FindControl("CommandCtrl") as Home.CommandCtrl;
			commandCtrl.OnCommand += new Home.CommandCtrl.OnCommandDelegate(commandCtrl_OnCommand);
		}

		static String ArticleRowHtmlFormat =
		@"
		<tr>
			<td class='article_title'><a href='../article.aspx?ID={3}'>{0}</a></th>
			<td class='article_renewtime'>{1:yyyy-MM-dd HH:mm}</th>
			<td class='article_state'>{2}</th>
			<td class='article_istop'>{5}</th>
			<td class='article_operation'><a href='edit.aspx?ID={3}'>编辑</a><a href='javascript:Delete({3},{4})'>删除</a></th>
		</tr>";

		public String RenderArticlesTable()
		{
			StringBuilder builder = new StringBuilder();
			if (Global.CurrentUser != null)
			{
				foreach (DataRow row in BlogDb.Instance.GetArticles(Global.CurrentUser.ID))
				{
					builder.AppendFormat(
						ArticleRowHtmlFormat,
						row["Title"], row["RenewTime"],
						Convert.ToInt64(row["IsPost"]) == 0 ? "草稿" : "已发布",
						row["ID"],
						Utility.RenderJson(row["Title"]),
						Convert.ToInt64(row["IsTop"]) == 1 ? "是" : "否"
					);
				}
			}
			return builder.ToString();
		}

		void commandCtrl_OnCommand(string command, object data)
		{
			if (command == "Delete")
			{
				BlogDb.Instance.DeleteArticle(Convert.ToInt64(data));
			}
		}
	}
}