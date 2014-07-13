﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using LUCC;

namespace Home
{
	public partial class blog_manage_logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
			Response.Redirect("/blog/index.aspx");
		}
	}
}