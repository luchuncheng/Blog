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
	public partial class blog_edit : System.Web.UI.Page
	{
		protected DataRow _articleInfo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			Home.CommandCtrl ctrl = Master.Master.FindControl("CommandCtrl") as Home.CommandCtrl;

			Int64 articleId = 0;
			if (Request.QueryString["ID"] != null)
			{
				ctrl.State["ID"] = Convert.ToInt64(Request.QueryString["ID"]);
			}
			if (ctrl.State["ID"] != null) articleId = Convert.ToInt64(ctrl.State["ID"]);

			if (IsPostBack)
			{
				try
				{
					if (Request.Params["post"] != null || Request.Params["save"] != null)
					{
						if (articleId > 0)
						{
							BlogDb.Instance.UpdateArticle(
								articleId,
								Global.CurrentUser.ID,
								Request.Params["article_title"],
								Request.Params["article_summary"],
								Request.Params["article_content"],
								Convert.ToInt64(Request.Params["atricle_catetory"]),
								Request.Params["article_istop"] != null,
								Request.Params["post"] != null
							);
							if (Request.Params["post"] != null)
							{
								Response.Redirect(String.Format("../article.aspx?ID={0}", articleId));
							}
							else
							{
								ctrl.State["Error"] = "Alert";
								ctrl.State["Exception"] = "已保存草稿";
							}
						}
						else
						{
							_articleInfo = BlogDb.Instance.NewArticle(
								Request.Params["article_title"],
								Request.Params["article_summary"],
								Request.Params["article_content"],
								Global.CurrentUser.ID,
								Convert.ToInt64(Request.Params["atricle_catetory"]),
								Request.Params["article_istop"] != null,
								Request.Params["post"] != null
							);
							articleId = Convert.ToInt64(_articleInfo["ID"]);
							if (Request.Params["post"] != null)
							{
								Response.Redirect(String.Format("../article.aspx?ID={0}", _articleInfo["ID"]));
							}
							else
							{
								ctrl.State["Error"] = "Alert";
								ctrl.State["Exception"] = "已保存草稿";
							}
						}
					}
				}
				catch (Exception ex)
				{
					ctrl.State["Error"] = "Alert";
					ctrl.State["Exception"] = ex;
				}
			}
			if (articleId > 0)
			{
				_articleInfo = BlogDb.Instance.GetArticleDetails(articleId);
				ctrl.State["ID"] = articleId;
			}
			Control c = Master.Master.FindControl("Content").FindControl("BlogContent").FindControl("PlaceHolder1");
			c.Visible = _articleInfo == null || Convert.ToInt64(_articleInfo["IsPost"]) == 0;
		}

		protected String RenderSelectItems()
		{
			StringBuilder builder = new StringBuilder();
			foreach (DataRow row in BlogDb.Instance.GetAllCategory(Global.CurrentUser.ID))
			{
				builder.AppendFormat(
					"<option value='{0}'>{1}</option>\r\n",
					row["ID"], Utility.TransferCharForXML(row["Title"].ToString())
				);
			}
			return builder.ToString();
		}
	}
}