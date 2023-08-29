using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Repositories
{
	public class RespositoryUser
	{
		private SqlConnection _sqlConnection;
		private SqlDataReader _sqlDataReader;
		private SqlCommand _sqlCommand;
		private readonly string _sqlTextConnection;

		public RespositoryUser()
		{
			this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
		}

		public UserFkw GetUser(string userName)
		{
			UserFkw userResult = null;
			string sqlGetUser = @"Select Id, UserName, Password, DateCreated, UserCreated, LoginSessions, UserBlocked,
                                ApplyToken, RolId from UserFkw where UserName = @userName;";
			this._sqlCommand = new SqlCommand();
			using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
			{
				this._sqlCommand = new SqlCommand();
				this._sqlCommand.Connection = this._sqlConnection;
				this._sqlConnection.Open();
				this._sqlCommand.CommandText = sqlGetUser;
				this._sqlCommand.Parameters.AddWithValue("userName", userName);
				this._sqlDataReader = this._sqlCommand.ExecuteReader();

				if (this._sqlDataReader.HasRows)
				{
					this._sqlDataReader.Read();

					userResult = UserFkw.Create(this._sqlDataReader.GetGuid(0),
						this._sqlDataReader.GetString(1), this._sqlDataReader.GetString(2),
						this._sqlDataReader.GetDateTime(3), this._sqlDataReader.GetGuid(4),
						this._sqlDataReader.GetInt32(5), this._sqlDataReader.GetBoolean(6),
						this._sqlDataReader.GetBoolean(7), this._sqlDataReader.GetGuid(8));

				}

			}

			return userResult;
		}

		public UserFkw GetUser(Guid id)
		{
			UserFkw userResult = null;
			string sqlGetUser = @"Select Id, UserName, Password, DateCreated, UserCreated, LoginSessions, UserBlocked,
                                ApplyToken, RolId from UserFkw where Id = @id;";
			this._sqlCommand = new SqlCommand();
			using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
			{
				this._sqlCommand = new SqlCommand();
				this._sqlCommand.Connection = this._sqlConnection;
				this._sqlConnection.Open();
				this._sqlCommand.CommandText = sqlGetUser;
				this._sqlCommand.Parameters.AddWithValue("id", id);
				this._sqlDataReader = this._sqlCommand.ExecuteReader();

				if (this._sqlDataReader.HasRows)
				{
					this._sqlDataReader.Read();

					userResult = UserFkw.Create(this._sqlDataReader.GetGuid(0),
						this._sqlDataReader.GetString(1), this._sqlDataReader.GetString(2),
						this._sqlDataReader.GetDateTime(3), this._sqlDataReader.GetGuid(4),
						this._sqlDataReader.GetInt32(5), this._sqlDataReader.GetBoolean(6),
						this._sqlDataReader.GetBoolean(7), this._sqlDataReader.GetGuid(8));

				}

			}

			return userResult;
		}

		public int Save(UserFkw newUser)
		{

			int result;
			string sqlGetUser = "INSERT INTO UserFkw VALUES(@id, @userName, @password, @dateCreated, @userCreated, @loginSessions,@rolId, @applyToken, @userBlocked);";
			this._sqlCommand = new SqlCommand();
			using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
			{
				this._sqlCommand = new SqlCommand();
				this._sqlCommand.Connection = this._sqlConnection;
				this._sqlConnection.Open();
				this._sqlCommand.CommandText = sqlGetUser;
				this._sqlCommand.Parameters.AddWithValue("Id", newUser.Id);
				this._sqlCommand.Parameters.AddWithValue("userName", newUser.UserName);
				this._sqlCommand.Parameters.AddWithValue("password", newUser.Password);
				this._sqlCommand.Parameters.AddWithValue("dateCreated", newUser.DateCreated);
				this._sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserCreated);
				this._sqlCommand.Parameters.AddWithValue("loginSessions", newUser.LoginSessions);
				this._sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserBlocked);
				this._sqlCommand.Parameters.AddWithValue("rolId", newUser.RolId);
				this._sqlCommand.Parameters.AddWithValue("applyToken", newUser.ApplyToken);

				result = this._sqlCommand.ExecuteNonQuery();

			}

			return result;

		}

		public bool Delete(Guid userId)
		{

			bool deleteUserFkw, deleteUserInfo, deleteUserTokens;
			this._sqlCommand = new SqlCommand();
			SqlTransaction sqlTransaction = null;

			try
			{
				this._sqlConnection = new SqlConnection(this._sqlTextConnection);


				this._sqlConnection.Open();
				sqlTransaction = this._sqlConnection.BeginTransaction();
				this._sqlCommand = new SqlCommand();
				this._sqlCommand.Connection = this._sqlConnection;
				this._sqlCommand.Transaction = sqlTransaction;

				this._sqlCommand.CommandText = "DELETE FROM UserFkw WHERE Id = @Id";
				this._sqlCommand.Parameters.AddWithValue("Id", userId);
				deleteUserFkw = this._sqlCommand.ExecuteNonQuery() > 0;


				this._sqlCommand.CommandText = "DELETE FROM UserInformation WHERE idUser = @Id";
				deleteUserInfo = this._sqlCommand.ExecuteNonQuery() > 0;


				this._sqlCommand.CommandText = "DELETE FROM UserToken WHERE id = @Id";
				deleteUserTokens = this._sqlCommand.ExecuteNonQuery() > 0;

				sqlTransaction.Commit();


			}
			catch (Exception e)
			{
				sqlTransaction.Rollback();
				
				throw e;
			}
			finally
			{
				this._sqlConnection.Close();
			}

			return deleteUserFkw || deleteUserInfo || deleteUserTokens;

		}

		public int UpdatePassword(Guid userId, string newPassword)
		{

			int result;
			string sqlGetUser = "UPDATE UserFkw SET Password = @password WHERE Id = @id";
			this._sqlCommand = new SqlCommand();
			using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
			{
				this._sqlCommand = new SqlCommand();
				this._sqlCommand.Connection = this._sqlConnection;
				this._sqlConnection.Open();
				this._sqlCommand.CommandText = sqlGetUser;
				this._sqlCommand.Parameters.AddWithValue("id", userId);
				this._sqlCommand.Parameters.AddWithValue("password", newPassword);
				result = this._sqlCommand.ExecuteNonQuery();

			}

			return result;

		}

		public int Update(UserFkw newUser)
		{

			int result;
			string sqlGetUser = @"
             UPDATE dbo.UserFkw
             SET 
	           UserName = @userName
              ,Password = @password
              ,DateCreated = @dateCreated
              ,UserCreated = @userCreated
              ,LoginSessions = @loginSessions
              ,UserBlocked = @userBlocked
             WHERE Id = @Id;
            ";
			this._sqlCommand = new SqlCommand();
			using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
			{
				this._sqlCommand = new SqlCommand();
				this._sqlCommand.Connection = this._sqlConnection;
				this._sqlConnection.Open();
				this._sqlCommand.CommandText = sqlGetUser;
				this._sqlCommand.Parameters.AddWithValue("Id", newUser.Id);
				this._sqlCommand.Parameters.AddWithValue("userName", newUser.UserName);
				this._sqlCommand.Parameters.AddWithValue("password", newUser.Password);
				this._sqlCommand.Parameters.AddWithValue("dateCreated", newUser.DateCreated);
				this._sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserCreated);
				this._sqlCommand.Parameters.AddWithValue("loginSessions", newUser.LoginSessions);
				this._sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserBlocked);
				this._sqlCommand.Parameters.AddWithValue("rolId", newUser.RolId);
				this._sqlCommand.Parameters.AddWithValue("applyToken", newUser.ApplyToken);
				result = this._sqlCommand.ExecuteNonQuery();

			}

			return result;

		}


	}
}
