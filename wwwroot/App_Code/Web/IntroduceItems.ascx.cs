using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Home
{
	public partial class IntroduceItems : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public String AppPath
		{
			get { return LUCC.Global.BaseUrl; }
		}
	}
}