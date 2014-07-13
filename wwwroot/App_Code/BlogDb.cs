using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Web;
using Core;

namespace LUCC
{
	public class AccountInfo
	{
		public Int32 ID;
		public String Name;
		public String Nickname;
		public String HeadIMG;
		public String EMail;
	}

	public class BlogDb
	{
		private static BlogDb m_Instance = new BlogDb();

		public static BlogDb Instance
		{
			get { return m_Instance; }
		}

		private BlogDb()
		{
		}

		static string ConnectionString
		{
			get
			{
				return String.Format("Data Source=\"{0}\";Pooling=False", Utility.GetDataPath("Db\\Blog.db"));
			}
		}

		static BlogDb()
		{
		}

		public DataRowCollection GetAllCategory(Int64 owner)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select * from Category where (AuthorID = 0 or AuthorID = ?) and ParentID >= 0;
				"
			);

			command.Parameters.Add("AuthorID", DbType.Int64).Value = owner;

			DataTable dt = new DataTable();
			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;
			ada.Fill(dt);
			ada.Dispose();
			return dt.Rows;
		}

		public DataRowCollection GetForumCategories()
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select 
						*, 
						(select count(Article.ID) from Article where CategoryID = Category.ID) as ThemeCount, 
						(select count(c.ID) from Comment c, Article a where c.ArticleID = a.ID and a.CategoryID = Category.ID) as CommentCount,
						(select max(Article.CreatedTime) from Article where CategoryID = Category.ID) as ThemeMaxCreatedTime, 
						(select max(c.CreatedTime) from Comment c, Article a where c.ArticleID = a.ID and a.CategoryID = Category.ID) as CommentMaxCreatedTime
					from Category
					where ParentID = 2;
				"
			);

			DataTable dt = new DataTable();
			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;
			ada.Fill(dt);
			ada.Dispose();
			return dt.Rows;
		}

		public DataRowCollection GetArticles(Int64 owner)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select a.ID, a.Title, a.Summary, a.RenewTime, c.Title, ? as AuthorName, a.IsPost, a.IsTop
					from Article a, Category c
					where a.CategoryID = c.ID and a.AuthorID = ?
					order by a.IsTop desc, a.CreatedTime desc;
				"
			);

			command.Parameters.Add("AuthorName", DbType.String).Value = BlogDb.Instance.GetAccountInfo(owner).Nickname;
			command.Parameters.Add("AuthorID", DbType.Int64).Value = owner;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;

			DataTable dt = new DataTable();
			ada.Fill(dt);

			ada.Dispose();
			return dt.Rows;
		}

		public DataRowCollection GetThemes(Int64 categoryId)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select 
						a.ID, a.Title, a.RenewTime, a.AuthorID,
						(select count(Comment.ID) from Comment where ArticleID = a.ID) as CommentCount
					from Article a, Category c
					where a.CategoryID = c.ID and c.ID = ?
					order by a.IsTop desc, a.CreatedTime desc;
				"
			);

			command.Parameters.Add("CategoryID", DbType.Int64).Value = categoryId;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;

			DataTable dt = new DataTable();
			ada.Fill(dt);

			ada.Dispose();
			return dt.Rows;
		}

		public DataRowCollection GetLeatestArticles(Int64 owner)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select a.ID, a.Title, a.Summary, a.RenewTime, c.Title, ? as AuthorName, a.IsPost, a.IsTop
					from Article a, Category c
					where a.CategoryID = c.ID and a.AuthorID = ? and IsPost = 1
					order by a.IsTop desc, a.CreatedTime desc
					Limit 0,5;
				"
			);

			command.Parameters.Add("AuthorName", DbType.String).Value = BlogDb.Instance.GetAccountInfo(owner).Nickname;
			command.Parameters.Add("AuthorID", DbType.Int64).Value = owner;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;

			DataTable dt = new DataTable();
			ada.Fill(dt);

			ada.Dispose();
			return dt.Rows;
		}

		public DataRowCollection GetPostArticles(Int64 owner)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select a.ID, a.Title, a.Summary, a.RenewTime, c.Title, ? as AuthorName, a.IsPost, a.IsTop
					from Article a, Category c
					where a.CategoryID = c.ID and a.AuthorID = ? and IsPost = 1
					order by a.IsTop desc, a.CreatedTime desc;
				"
			);

			command.Parameters.Add("AuthorName", DbType.String).Value = BlogDb.Instance.GetAccountInfo(owner).Nickname;
			command.Parameters.Add("AuthorID", DbType.Int64).Value = owner;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;

			DataTable dt = new DataTable();
			ada.Fill(dt);

			ada.Dispose();
			return dt.Rows;
		}

		public void UpdateArticle(Int64 id,Int64 owner, string title, string summary, string content, Int64 categoryId, bool isTop,bool isPost)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			try
			{
				SQLiteCommand command = new SQLiteCommand();
				command.Connection = conn;
				command.CommandText = String.Format(
					@"
					UPDATE Article SET Title=?, Summary=?, Content=?, CategoryID=?, IsTop=?, RenewTime=?, IsPost=?
					where ID = ? and AuthorID = ?;
					"
				);

				command.Parameters.Add("Title", DbType.String).Value = title;
				command.Parameters.Add("Summary", DbType.String).Value = summary;
				command.Parameters.Add("Content", DbType.String).Value = content;
				command.Parameters.Add("CategoryId", DbType.Int64).Value = categoryId;
				command.Parameters.Add("IsTop", DbType.Int64).Value = isTop ? 1 : 0;
				command.Parameters.Add("RenewTime", DbType.DateTime).Value = DateTime.Now;
				command.Parameters.Add("IsPost", DbType.Int64).Value = isPost ? 1 : 0;
				command.Parameters.Add("ID", DbType.Int64).Value = id;
				command.Parameters.Add("AuthorID", DbType.Int64).Value = owner;

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		public DataRow NewArticle(string title, string summary, string content, Int64 author, Int64 categoryId, bool isTop, bool isPost)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					insert into Article (Title, Summary, Content, AuthorID, CategoryID, IsTop,IsPost, CreatedTime, RenewTime)
					values (?,?,?,?,?,?,?,?,?);
					select * from Article where ID = last_insert_rowid();"
			);

			command.Parameters.Add("Title", DbType.String).Value = title;
			command.Parameters.Add("Summary", DbType.String).Value = summary;
			command.Parameters.Add("Content", DbType.String).Value = content;
			command.Parameters.Add("AuthorID", DbType.Int64).Value = author;
			command.Parameters.Add("CategoryId", DbType.Int64).Value = categoryId;
			command.Parameters.Add("IsTop", DbType.Int64).Value = isTop ? 1 : 0;
			command.Parameters.Add("IsPost", DbType.Int64).Value = isPost ? 1 : 0;
			command.Parameters.Add("CreateTime", DbType.DateTime).Value = DateTime.Now;
			command.Parameters.Add("RenewTime", DbType.DateTime).Value = DateTime.Now;

			DataTable dt = new DataTable();
			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;
			ada.Fill(dt);
			ada.Dispose();
			return dt.Rows.Count > 0 ? dt.Rows[0] : null;
		}

		public DataRow GetArticleDetails(Int64 id)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"select * from Article where ID = ?;"
			);

			command.Parameters.Add("ID", DbType.Int64).Value = id;

			DataTable dt = new DataTable();
			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;
			ada.Fill(dt);
			ada.Dispose();
			return dt.Rows.Count > 0 ? dt.Rows[0] : null;
		}

		public void DeleteArticle(Int64 id)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			try
			{
				SQLiteCommand command = new SQLiteCommand();
				command.Connection = conn;
				command.CommandText = String.Format(
					@"delete from Article where ID = ?;"
				);

				command.Parameters.Add("ID", DbType.Int64).Value = id;

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Clone();
			}
		}

		public DataRowCollection GetComments(Int64 articleId)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"select c.*, a.AuthorID as OwnerID from Comment c, Article a where c.ArticleID = ? and c.ArticleID = a.ID"
			);

			command.Parameters.Add("ArticleID", DbType.Int64).Value = articleId;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;

			DataTable dt = new DataTable();
			ada.Fill(dt);

			ada.Dispose();
			return dt.Rows;
		}

		public DataRow NewComment(string name, Int64 authorId,Int64 articleId, string email, string content,Int64 replyTo)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					insert into Comment (ArticleID, AuthorID, AuthorName, EMail, CreatedTime, ReplyTo, Content)
					values (?,?,?,?,datetime('now','localtime'),?,?);
					select * from Comment where ID = last_insert_rowid();"
			);

			command.Parameters.Add("articleId", DbType.Int64).Value = articleId;
			command.Parameters.Add("authorId", DbType.Int64).Value = authorId;
			command.Parameters.Add("name", DbType.String).Value = name;
			command.Parameters.Add("email", DbType.String).Value = email;
			command.Parameters.Add("replyTo", DbType.Int64).Value = replyTo;
			command.Parameters.Add("content", DbType.String).Value = content;
			command.Parameters.Add("replyTo2", DbType.Int64).Value = replyTo;

			DataTable dt = new DataTable();
			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;
			ada.Fill(dt);
			ada.Dispose();
			return dt.Rows.Count > 0 ? dt.Rows[0] : null;
		}

		public void DeleteComment(Int64 id)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			try
			{
				SQLiteCommand command = new SQLiteCommand();
				command.Connection = conn;
				command.CommandText = String.Format(
					@"delete from Comment where ID = ?;"
				);

				command.Parameters.Add("ID", DbType.Int64).Value = id;

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Clone();
			}
		}

		public DataRowCollection GetLeatestComments(Int64 owner)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand command = new SQLiteCommand();
			command.Connection = conn;
			command.CommandText = String.Format(
				@"
					select c.*, a.Title as Title
					from Comment c, Article a
					where c.ArticleID = a.ID and a.AuthorID = ?
					order by c.CreatedTime desc
					Limit 0, 5;
				"
			);

			command.Parameters.Add("AuthorID", DbType.Int64).Value = owner;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = command;

			DataTable dt = new DataTable();
			ada.Fill(dt);

			ada.Dispose();
			return dt.Rows;
		}

		public AccountInfo GetAccountInfo(string name)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;
			cmd.CommandText = string.Format(
				"select [Key] as ID, Name, Nickname, HeadIMG, EMail from Users u where u.UpperName=?",
				name
			);

			cmd.Parameters.Add("User", DbType.String).Value = name.ToUpper();

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = cmd;

			DataTable dt = new DataTable();
			ada.Fill(dt);
			ada.Dispose();

			if (dt.Rows.Count > 0)
			{
				DataRow row = dt.Rows[0];
				AccountInfo info = new AccountInfo();
				info.ID = Convert.ToInt32(row["ID"]);
				info.Name = Convert.ToString(row["Name"]);
				info.Nickname = Convert.ToString(row["Nickname"]);
				info.HeadIMG = Convert.ToString(row["HeadIMG"]);
				info.EMail = Convert.ToString(row["EMail"]);
				return info;
			}
			else
			{
				return null;
			}
		}

		public AccountInfo GetAccountInfo(Int64 key)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;
			cmd.CommandText = "select [Key] as ID, Name, Nickname, HeadIMG, EMail from Users u where u.Key=?";

			cmd.Parameters.Add("User", DbType.Int64).Value = key;

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = cmd;

			DataTable dt = new DataTable();
			ada.Fill(dt);
			ada.Dispose();

			if (dt.Rows.Count > 0)
			{
				DataRow row = dt.Rows[0];
				AccountInfo info = new AccountInfo();
				info.ID = Convert.ToInt32(row["ID"]);
				info.Name = Convert.ToString(row["Name"]);
				info.Nickname = Convert.ToString(row["Nickname"]);
				info.HeadIMG = Convert.ToString(row["HeadIMG"]);
				info.EMail = Convert.ToString(row["EMail"]);
				return info;
			}
			else
			{
				return null;
			}
		}

		public bool Validate(string userId, string password)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;
			cmd.CommandText = string.Format(
				"select * from Users where UpperName=? and Password=?",
				userId, password
			);

			cmd.Parameters.Add("User", DbType.String).Value = userId.ToUpper();
			cmd.Parameters.Add("Password", DbType.String).Value = Core.Utility.MD5(password);

			SQLiteDataAdapter ada = new SQLiteDataAdapter();
			ada.SelectCommand = cmd;

			DataTable dt = new DataTable();
			ada.Fill(dt);
			ada.Dispose();

			return dt.Rows.Count > 0;

		}

		public void CreateUser(String name, String nickname, String password, String email)
		{
			SQLiteConnection conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			try
			{
				SQLiteTransaction tran = conn.BeginTransaction();
				try
				{
					SQLiteCommand selectCmd = new SQLiteCommand(
						@"select Key from Users where UpperName=?", conn
					);
					selectCmd.Parameters.Add("Name", DbType.String).Value = name.ToUpper();
					object key = selectCmd.ExecuteScalar();
					if (key != null && key != DBNull.Value)
					{
						throw new Exception(String.Format("用户\"{0}\"已存在！", name));
					}

					SQLiteCommand insertUser = new SQLiteCommand(
						@"
							insert into Users (Name,UpperName,Password,Nickname,Type,EMail,InviteCode,IsTemp,RegisterTime) values (?,?,?,?,?,?,?,0,?)
							", conn
					);
					insertUser.Parameters.Add("Name", DbType.String).Value = name;
					insertUser.Parameters.Add("UpperName", DbType.String).Value = name.ToUpper();
					insertUser.Parameters.Add("Password", DbType.String).Value = Core.Utility.MD5(password);
					insertUser.Parameters.Add("Nickname", DbType.String).Value = nickname;
					insertUser.Parameters.Add("Type", DbType.Int64).Value = 0;
					insertUser.Parameters.Add("EMail", DbType.String).Value = email;
					insertUser.Parameters.Add("InviteCode", DbType.String).Value = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
					insertUser.Parameters.Add("RegisterTime", DbType.DateTime).Value = DateTime.Now;
					insertUser.ExecuteNonQuery();

					SQLiteCommand insertUR = new SQLiteCommand(
						@"
							insert into User_Role (UserKey,RoleKey)
							select Key as UserKey,2 as RoleKey from Users where UpperName=?;
						",
						conn
					);
					string upperName = name.ToUpper();
					insertUR.Parameters.Add("Name", DbType.String).Value = upperName;

					insertUR.ExecuteNonQuery();

					tran.Commit();
				}
				catch
				{
					tran.Rollback();
					throw;
				}
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
