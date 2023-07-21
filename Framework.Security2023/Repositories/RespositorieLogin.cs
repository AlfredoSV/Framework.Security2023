using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Repositories
{
    public class RespositorieUser
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RespositorieUser(string sqlTextConnection= "server=ALFREDO ; database=Framework_Users ; integrated security = true")
        {
            this._sqlTextConnection = sqlTextConnection;
        }

        public User GetUser(string userName)
        {
            User userResult = new User();
            string sqlGetUser = @"Select Id, UserName, Password, DateCreated, UserCreated, LoginSessions, UserBlocked 
                                from Users where UserName = @userName;";
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

                    userResult = new User()
                    {

                        Id = this._sqlDataReader.GetGuid(0),
                        UserName = this._sqlDataReader.GetString(1),
                        Password = this._sqlDataReader.GetString(2),
                        DateCreated = this._sqlDataReader.GetDateTime(3),
                        UserCreated = this._sqlDataReader.GetGuid(4),
                        LoginSessions = this._sqlDataReader.GetInt32(5),
                        UserBlocked = this._sqlDataReader.GetBoolean(6)

                    };

                }
                else
                    userResult = null;
              

            }

            return userResult;
        }

        public int Save(User newUser)
        {

            int result;
            string sqlGetUser = "INSERT INTO Users VALUES(@id, @userName, @password, @dateCreated, @userCreated, @loginSessions, @userBlocked);";
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
                this._sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserCreated);
                result =  this._sqlCommand.ExecuteNonQuery();

            }

            return result;

        }
    }
}
