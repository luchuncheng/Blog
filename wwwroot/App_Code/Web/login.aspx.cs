using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using LUCC;
using Core;

namespace Home
{
	public partial class login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				if (Request.Params["login"] != null)
				{
					if (BlogDb.Instance.Validate(Request.Params["name"], Request.Params["password"]))
					{
						AccountInfo info = BlogDb.Instance.GetAccountInfo(Request.Params["name"]);
						AddCookie(info.Name as string, false);
						if (Request.Params["ReturnUrl"] != null) Response.Redirect(Request.Params["ReturnUrl"]);
						else Response.Redirect("/default.aspx");
					}
				}
			}
		}

		void AddCookie(string userName, bool isPresistent)
		{
			FormsAuthentication.Initialize();

			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				2,
				userName,
				DateTime.Now,
				DateTime.Now.AddMonths(1),
				isPresistent,
				"",
				FormsAuthentication.FormsCookiePath
			);

			string hash = FormsAuthentication.Encrypt(ticket);
			HttpCookie cookie = new HttpCookie(
				FormsAuthentication.FormsCookieName,
				hash
			);

			if (ticket.IsPersistent)
			{
				cookie.Expires = ticket.Expiration;
			}

			Response.Cookies.Add(cookie);
		}
	}
}