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
	public partial class blog_index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		static String ArticleHtmlFormat =
		@"			
		<div class='entry'>
			<h3><a href='article.aspx?ID={2}'>{0}</a></h3>
			<div style='text-indent: 2em;'>
			{1}
			</div>
			<p><a class='more-link' href='article.aspx?ID={2}'>阅读全文</a></p>
		</div>
		";
		protected String RenderArticles()
		{
			StringBuilder builder = new StringBuilder();
			AccountInfo user = BlogDb.Instance.GetAccountInfo(Request.QueryString["User"] ?? "admin");
			foreach (DataRow row in LUCC.BlogDb.Instance.GetPostArticles(user.ID))
			{
				builder.AppendFormat(ArticleHtmlFormat, row["Title"], row["Summary"], row["ID"]);
			}
			return builder.ToString();
		}
	}
}