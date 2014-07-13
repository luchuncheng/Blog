using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace LUCC
{
	public delegate void ApplicationStartDelegate(System.Web.HttpApplication app, EventArgs e);

	public class Global
	{
		public static String BaseUrl
		{
			get 
			{ 
				return System.Web.HttpContext.Current.Request.ApplicationPath == "/" ? "" : System.Web.HttpContext.Current.Request.ApplicationPath; 
			}
		}

		public static String LocalPath
		{
			get 
			{ 
				return System.Web.HttpContext.Current.Server.MapPath(BaseUrl == "" ? "/" : BaseUrl); 
			}
		}

		public static String Version
		{
			get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		public static AccountInfo CurrentUser
		{
			get
			{
				return BlogDb.Instance.GetAccountInfo(System.Web.HttpContext.Current.User.Identity.Name);
			}
		}

		public static ApplicationStartDelegate ApplicationStart;

	}
}
