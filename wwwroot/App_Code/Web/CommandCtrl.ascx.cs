using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections;
using Core;

namespace Home
{
	public partial class CommandCtrl : System.Web.UI.UserControl
	{
		public delegate void OnCommandDelegate(string cmd, object data);

		public event OnCommandDelegate OnCommand;

		private Hashtable _state = new Hashtable();

		public Hashtable State
		{
			get { return _state; }
		}

		public CommandCtrl()
		{
			OnCommand += new OnCommandDelegate(CommandCtrl_OnCommand);
		}

		private void CommandCtrl_OnCommand(string command, object data)
		{
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Request.Params[ClientID + "_state_json"]))
			{
				_state = Utility.ParseJson(Request.Params[ClientID + "_state_json"]) as Hashtable;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Request.Params[ClientID + "_command"]))
			{
				OnCommand(
					Request.Params[ClientID + "_command"],
					Utility.ParseJson(Request.Params[ClientID + "_data"])
				);
			}
		}

		protected String StateJson
		{
			get { return Utility.RenderJson(_state).Replace("\"", "&quot;").Replace("<", "&lt;"); }
		}

		protected String StateVarName
		{
			get { return "__" + ClientID + "_state"; }
		}
	}
}