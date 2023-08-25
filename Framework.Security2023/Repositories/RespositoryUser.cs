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

        public RespositoryUser(){
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
                this._sqlCommand.Parameters.AddWithValue("password", newUser.UserName);
                this._sqlCommand.Parameters.AddWithValue("dateCreated", newUser.DateCreated);
                this._sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserCreated);
                this._sqlCommand.Parameters.AddWithValue("loginSessions", newUser.LoginSessions);
                this._sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserBlocked);
				this._sqlCommand.Parameters.AddWithValue("rolId", newUser.RolId);
				this._sqlCommand.Parameters.AddWithValue("applyToken", newUser.ApplyToken);

				result =  this._sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

        public int Delete(Guid userId)
        {

            int result;
            string sqlGetUser = "DELETE FROM Users WHERE Id = @Id";
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand();
                this._sqlCommand.Connection = this._sqlConnection;
                this._sqlConnection.Open();
                this._sqlCommand.CommandText = sqlGetUser;
                this._sqlCommand.Parameters.AddWithValue("Id", userId);
                result = this._sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

        public int UpdatePassword(Guid userId, string newPassword)
        {

            int result;
            string sqlGetUser = "UPDATE Users SET Password = @password WHERE Id = @id";
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
             UPDATE dbo.Users
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
                this._sqlCommand.Parameters.AddWithValue("password", newUser.UserName);
                this._sqlCommand.Parameters.AddWithValue("dateCreated", newUser.DateCreated);
                this._sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserCreated);
                this._sqlCommand.Parameters.AddWithValue("loginSessions", newUser.LoginSessions);
                this._sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserBlocked);
                result = this._sqlCommand.ExecuteNonQuery();

            }

            return result;

        }


    }
}
