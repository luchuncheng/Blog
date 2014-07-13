using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using LUCC;
using Core;

namespace Home
{
	public partial class blog_BlogMasterPage : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		static String ArticleLiHtmlFormat = "<li><a href='article.aspx?ID={0}' title='{1}'>{2}</a></li>";
		static String CommentLiHtmlFormat = "<li><a href='article.aspx?ID={0}#comment_{3}' title='{1}'><span>{2}</span></a></li>";

		protected String RenderLeatestAtricles()
		{
			StringBuilder builder = new StringBuilder();
			foreach (DataRow row in BlogDb.Instance.GetLeatestArticles(BlogDb.Instance.GetAccountInfo("admin").ID))
			{
				builder.AppendFormat(
					ArticleLiHtmlFormat,
					row["ID"],
					Utility.TransferCharForXML(row["Title"].ToString()),
					row["Title"]
				);
			}
			return builder.ToString();
		}
		protected String RenderLeatestComments()
		{
			StringBuilder builder = new StringBuilder();
			foreach (DataRow row in BlogDb.Instance.GetLeatestComments(BlogDb.Instance.GetAccountInfo("admin").ID))
			{
				string content = row["Content"].ToString();
				if (content.Length > 40) content = content.Substring(0, 40) + "...";
				builder.AppendFormat(
					CommentLiHtmlFormat,
					row["ArticleID"],
					Utility.TransferCharForXML(row["Title"].ToString()),
					content,
					row["ID"]
				);
			}
			return builder.ToString();
		}
	}
}