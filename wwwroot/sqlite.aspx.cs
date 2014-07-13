using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;
using System.Data;

public partial class sqlite : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source=\"{0}\";Pooling=False", Core.Utility.GetDataPath("Db\\Blog.db")));
		conn.Open();
		try
		{
			SQLiteCommand cmd = new SQLiteCommand(TextBox1.Text, conn);
			cmd.ExecuteNonQuery();
		}
		finally
		{
			conn.Clone();
		}
	}
}
