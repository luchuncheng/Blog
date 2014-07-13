using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUCC;

namespace Home
{
	public partial class index : MyPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
			}
			else
			{
				if (Request.Url.Host.ToLower() == "blog.luchuncheng.cn") Response.Redirect("http://www.lesktop.com/blog/index.aspx");
				else if (Request.Url.Host.ToLower() == "www.luchuncheng.cn") Response.Redirect("http://www.lesktop.com/blog/index.aspx");
				else if (Request.Url.Host.ToLower() == "luchuncheng.cn") Response.Redirect("http://www.lesktop.com/blog/index.aspx");
			}
		}
	}
}