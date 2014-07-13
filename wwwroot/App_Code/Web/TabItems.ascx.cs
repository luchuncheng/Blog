using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Home
{
	public partial class TabItems : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Control ctrl = FindControl("PlaceHolder1");
			ctrl.Visible = LUCC.Global.CurrentUser != null && LUCC.Global.CurrentUser.Name.ToLower() == "admin";
		}

		int _selectedIndex = 1;

		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set { _selectedIndex = value; }
		}

		public String AppPath
		{
			get { return LUCC.Global.BaseUrl; }
		}
	}
}