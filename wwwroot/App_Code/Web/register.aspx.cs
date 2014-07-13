using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUCC;
using Core;

namespace Home
{
	public partial class register : MyPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				if (Request.Params["register"] != null)
				{
					string name = Request.Params["name"];
					string password = Request.Params["password"];
					string nickname = Request.Params["nickname"];
					string email = Request.Params["email"];

					BlogDb.Instance.CreateUser(name, nickname, password, email);

					//BlogDb.Instance.AddAccount(name, password, nickname, email, 0);
				}
			}
		}
	}
}