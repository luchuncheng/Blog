using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using System.Data;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;
using Core;
using System.Data.SQLite;

namespace Core.Web
{
	class VerifyCodeHandler : IHttpHandler
	{
		static string VerifyCodeDbConnectionString
		{
			get
			{
				return string.Format("Data Source=\"{0}\";Pooling=False", Utility.GetDataPath("Db\\VerifyCode.db"));
			}
		}

		public VerifyCodeHandler()
		{
		}

		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			if (!String.IsNullOrEmpty(context.Request.Params["file"]))
			{
				string file = String.Format("{0}\\{1}", Core.Utility.GetDataPath("VerifyCode"), context.Request.Params["file"]);
				context.Response.AddHeader("Content-Type", "image/png");
				context.Response.WriteFile(file);
			}
			else
			{
				VerifyCode vc = new VerifyCode(context.Request.Params["Guid"]);
				SQLiteConnection conn = new SQLiteConnection(VerifyCodeDbConnectionString);
				conn.Open();
				try
				{
					SQLiteCommand command = new SQLiteCommand();
					command.Connection = conn;
					command.CommandText = String.Format(
						@"delete from VerifyCode where GUID = ? or (julianday(datetime('now', 'localtime'))*86400 - julianday(CreatedTime)*86400) > 1800;
					insert into VerifyCode (GUID, Code, CreatedTime) values (?,?,datetime('now', 'localtime'))"
					);

					command.Parameters.Add("GUID1", DbType.String).Value = vc.Guid.ToUpper();
					command.Parameters.Add("GUID2", DbType.String).Value = vc.Guid.ToUpper();
					command.Parameters.Add("Code", DbType.String).Value = vc.Code.ToLower();

					command.ExecuteNonQuery();
				}
				finally
				{
					conn.Clone();
				}
				context.Response.Write(String.Format("({{\"Guid\":\"{0}\"}})", vc.Guid));
			}
		}

		bool IHttpHandler.IsReusable
		{
			get { return true; }
		}

		public static bool CheckVerifyCode(String guid, String code)
		{
			SQLiteConnection conn = new SQLiteConnection(VerifyCodeDbConnectionString);
			conn.Open();
			try
			{
				SQLiteCommand command = new SQLiteCommand();
				command.Connection = conn;
				command.CommandText = String.Format(
					@"select Code from VerifyCode where GUID = ? and Code = ? and (julianday(datetime('now', 'localtime'))*86400 - julianday(CreatedTime)*86400) < 30 * 60"
				);

				command.Parameters.Add("GUID", DbType.String).Value = guid.ToUpper();
				command.Parameters.Add("Code", DbType.String).Value = code.ToLower();

				object r1 = command.ExecuteScalar();

				command.CommandText = String.Format(
					@"delete from VerifyCode where GUID = ?;
					select Code from VerifyCode where GUID = ?;"
				);

				command.Parameters.Add("GUID1", DbType.String).Value = guid.ToUpper();
				command.Parameters.Add("GUID2", DbType.String).Value = guid.ToUpper();

				object r2 = command.ExecuteScalar();

				return (r1 != null && r1 != DBNull.Value) && (r2 == null || r2 == DBNull.Value);
			}
			catch
			{
				return false;
			}
			finally
			{
				conn.Clone();
			}
		}
	}

	class VerifyCode
	{
		static string Letters = "abdefhijkmnprstuvwxy345678";
		static Color BkColor = Color.FromArgb(0x99, 0xCE, 0x28);
		static Brush BkBrush = new SolidBrush(BkColor);
		public String Code;
		public String Guid = System.Guid.NewGuid().ToString();
		public DateTime CreatedTime = DateTime.Now;
		public VerifyCode(String guid)
		{
			Guid = String.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
			Code = "";
			Random random = new Random();
			for (int i = 0; i < 4; i++)
			{
				Code += Letters[(int)Math.Round(random.NextDouble() * (Letters.Length - 1))];
			}
			Bitmap bmp = new Bitmap(76, 26);
			Graphics graphics = Graphics.FromImage(bmp);
			graphics.FillRectangle(BkBrush, new Rectangle(0, 0, bmp.Width, bmp.Height));
			graphics.DrawString(Code, new Font("Courier New", 22, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Blue, new RectangleF(4, 2, 68, 22));
			double h = Math.PI * 0.7;
			for (int i = 0; i < bmp.Height; i++)
			{
				int dif = 16 - (int)Math.Round(Math.Sin(h * i / (bmp.Height - 1)) * 16);
				for (int j = bmp.Width - 1; j >= 0; j--)
				{
					if (j < dif)
					{
						bmp.SetPixel(j, i, BkColor);
					}
					else
					{
						bmp.SetPixel(j, i, bmp.GetPixel(j - dif, i));
					}
				}
			}
			try
			{
				if (!System.IO.Directory.Exists(Core.Utility.GetDataPath("VerifyCode")))
				{
					System.IO.Directory.CreateDirectory(Core.Utility.GetDataPath("VerifyCode"));
				}
			}
			catch
			{
			}
			bmp.Save(String.Format("{0}\\{1}.png", Core.Utility.GetDataPath("VerifyCode"), Guid), ImageFormat.Png);
		}
	}
}
