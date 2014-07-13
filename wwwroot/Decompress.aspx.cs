using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.IO;

public partial class DecompressPage : System.Web.UI.Page
{
	class DecompressThreadParam
	{
		public string Source, Dest;
	}
	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected void btnUpdate_Click(object sender, EventArgs e)
	{
		DecompressThreadParam param = new DecompressThreadParam();
		param.Source = Server.MapPath("~") + "\\" + TextBox1.Text;
		param.Dest = Server.MapPath("~") + "\\" + TextBox2.Text;
		if (CheckBox1.Checked)
		{
			System.Threading.Thread thread = new System.Threading.Thread(DecompressThread);
			thread.Start(param);
		}
		else
		{
			DecompressThread(param);
		}
	}

	public void DecompressThread(object obj)
	{
		DecompressThreadParam param = obj as DecompressThreadParam;
		using (Stream stream = File.OpenRead(param.Source))
		{
			try
			{
				Decompress(stream, param.Dest);
			}
			catch
			{
			}
			stream.Close();
		}
	}

	public static void Decompress(Stream stream, string target)
	{
		if (!target.EndsWith("\\")) target += "\\";
		using (ZipInputStream inputStream = new ZipInputStream(stream))
		{
			try
			{
				ZipEntry theEntry;
				while ((theEntry = inputStream.GetNextEntry()) != null)
				{
					if (theEntry.IsDirectory)
					{
						Directory.CreateDirectory(target + theEntry.Name);
					}
					else if (theEntry.IsFile)
					{
						using (FileStream fileWrite = new FileStream(target + theEntry.Name, FileMode.Create, FileAccess.Write))
						{
							try
							{
								byte[] buffer = new byte[2048];
								int size;
								while (true)
								{
									size = inputStream.Read(buffer, 0, buffer.Length);
									if (size > 0)
										fileWrite.Write(buffer, 0, size);
									else
										break;
								}
							}
							catch (System.Exception)
							{
								throw;
							}
							finally
							{
								fileWrite.Close();
							}
						}
					}
				}
			}
			catch (System.Exception)
			{
				throw;
			}
			finally
			{
				inputStream.Close();
			}
		}
	}
}
