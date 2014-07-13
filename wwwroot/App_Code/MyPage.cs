using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class MyPage : System.Web.UI.Page
{
	public String AppPath
	{
		get { return LUCC.Global.BaseUrl; }
	}
}