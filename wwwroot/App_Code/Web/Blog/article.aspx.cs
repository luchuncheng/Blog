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
	public partial class blog_article : System.Web.UI.Page
	{
		DataRow _data;
		DataRowCollection _comments;
		protected void Page_Load(object sender, EventArgs e)
		{
			Home.CommandCtrl commandCtrl = Master.Master.FindControl("CommandCtrl") as Home.CommandCtrl;
			commandCtrl.OnCommand += new Home.CommandCtrl.OnCommandDelegate(commandCtrl_OnCommand);
			Int64 id = Convert.ToInt64(Request.QueryString["ID"]);
			_data = BlogDb.Instance.GetArticleDetails(id);
			if (IsPostBack)
			{
				if (Request.Params["submit_comment"] != null)
				{
					if (!Core.Web.VerifyCodeHandler.CheckVerifyCode(Request.Params["verifycode_guid"], Request.Params["comment_verifycode"]))
					{
						throw new Exception("验证码错误");
					}
					DataRow r = BlogDb.Instance.NewComment(
						Request.Params["comment_name"],
						Global.CurrentUser == null ? 0 : Global.CurrentUser.ID,
						id,
						Request.Params["comment_email"],
						HtmlUtil.ReplaceHtml(Request.Params["comment_message"]),
						0
					);
					Response.Redirect(string.Format("article.aspx?ID={0}#comment_{1}", id, r["ID"]));
				}
				else if (Request.Params["submit_reply_comment"] != null)
				{
					if (!Core.Web.VerifyCodeHandler.CheckVerifyCode(Request.Params["verifycode_guid"], Request.Params["reply_verifycode"]))
					{
						throw new Exception("验证码错误");
					}
					DataRow r = BlogDb.Instance.NewComment(
						Request.Params["reply_name"],
						Global.CurrentUser == null ? 0 : Global.CurrentUser.ID,
						id,
						Request.Params["reply_email"],
						HtmlUtil.ReplaceHtml(Request.Params["reply_message"]),
						commandCtrl.State["ReplyTo"] == null ? 0 : Convert.ToInt64(commandCtrl.State["ReplyTo"])
					);
					Response.Redirect(string.Format("article.aspx?ID={0}#comment_{1}", id, r["ID"]));
				}
			}

		}

		void commandCtrl_OnCommand(string command, object data)
		{
			if (command == "DeleteComment")
			{
				BlogDb.Instance.DeleteComment(Convert.ToInt64(data));
				Response.Redirect(Request.Url.ToString());
			}
		}

		static String CommentLiHtmlFormat = @"
		<a name='comment_{6}' style='display:block; height: 0px;'></a>
		<li class='{4}'>	
			<cite>
				<img alt='' src='{0}/{8}' class='avatar' height='40' width='40' />	
				<div style='float:left'>
				{1}<br />			
				<span class='comment-data'>{2:yyyy-MM-dd HH:mm}</span>
				</div>
				<div style='float:right;font-weight:normal;' class='operation'>
				<a href='javascript:ReplyTo({6})'>回复</a>
				{5}		
				</div>
			</cite>
			<div class='comment-text' style='text-indent:2em;'>
				<p>{3}</p>
			</div>
			{7}
			<div id='replyform_container_{6}'></div>
		</li>";

		protected String RenderCommentHtml(int c, Int64 replyTo)
		{
			if (_comments == null) _comments = BlogDb.Instance.GetComments(Convert.ToInt64(_data["ID"]));
			StringBuilder builder = new StringBuilder();
			builder.Append("<ol class='commentlist'>");
			int count = 0;
			foreach (DataRow row in _comments)
			{
				if (Convert.ToInt64(row["ReplyTo"]) != replyTo) continue;
				AccountInfo authorInfo = BlogDb.Instance.GetAccountInfo(Convert.ToInt64(row["AuthorID"]));
				string summary = row["Content"].ToString().Length > 10 ? row["Content"].ToString().Substring(0, 10) + "..." : row["Content"].ToString();
				builder.AppendFormat(
					CommentLiHtmlFormat,
					Global.BaseUrl,
					Utility.TransferCharForXML(row["AuthorName"].ToString()),
					row["CreatedTime"],
					row["Content"].ToString(),
					(count + c) % 2 == 0 ? "alt" : "",
					Global.CurrentUser != null && Convert.ToInt64(row["OwnerID"]) == Global.CurrentUser.ID ? String.Format("<a href='javascript:DeleteComment({0},{1})'>删除</a>", row["ID"], Utility.RenderJson(summary)) : "",
					row["ID"],
					RenderCommentHtml(count + c + 1, Convert.ToInt64(row["ID"])),
					authorInfo == null || authorInfo.HeadIMG == "" ? "images/gravatar.jpg" : String.Format("Lesktop/headimg.aspx?user={0}&size=40&gred=false", authorInfo.Name)
				);
				count++;
			}
			builder.Append("</ol>");
			return count > 0 ? builder.ToString() : "";
		}

		protected String CommentCount
		{
			get
			{
				if (_comments == null) _comments = BlogDb.Instance.GetComments(Convert.ToInt64(_data["ID"]));
				return _comments.Count.ToString();
			}
		}

		protected String ArticleID
		{
			get { return _data["ID"].ToString(); }
		}

		protected String ArticleTitle
		{
			get { return _data["Title"].ToString(); }
		}

		protected String Author
		{
			get
			{
				Int64 id = Convert.ToInt64(_data["AuthorID"]);
				return BlogDb.Instance.GetAccountInfo(id).Nickname;
			}
		}

		protected String Content
		{
			get { return _data["Content"].ToString(); }
		}

		protected String RenewTime
		{
			get { return String.Format("{0:yyyy-MM-dd HH:mm:ss}", _data["RenewTime"]); }
		}
	}
}