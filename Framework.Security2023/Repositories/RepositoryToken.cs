
using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    public class RepositoryToken
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryToken()
        {
            this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        public int Save(UserToken userToken)
        {
            int result;
            string sqlGetUser = "INSERT INTO UserToken " +
                "VALUES(@id, @userId, @token, @dateCreated, @dateExpiration);";
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand();
                this._sqlCommand.Connection = this._sqlConnection;
                this._sqlConnection.Open();
                this._sqlCommand.CommandText = sqlGetUser;
                this._sqlCommand.Parameters.AddWithValue("id", userToken.Id);
                this._sqlCommand.Parameters.AddWithValue("userId", userToken.UserId);
                this._sqlCommand.Parameters.AddWithValue("token", userToken.Token);
                this._sqlCommand.Parameters.AddWithValue("dateCreated", userToken.DateCreated);
                this._sqlCommand.Parameters.AddWithValue("dateExpiration", userToken.DateExpiration);
                result = this._sqlCommand.ExecuteNonQuery();

            }

            return result;


        }


        public UserToken GetLastToken(Guid id)
        {
            UserToken result = null;
            string sqlGetUser = @"Select Id,UserId, Token, DateCreated,
            DateExpiration from userToken where
            DateCreated = (Select MAX(DateCreated)
            from UserToken
            where userid = @userId)
            and userid = @userId;";
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand();
                this._sqlCommand.Connection = this._sqlConnection;
                this._sqlConnection.Open();
                this._sqlCommand.CommandText = sqlGetUser;
                this._sqlCommand.Parameters.AddWithValue("userId", id);
                this._sqlDataReader = this._sqlCommand.ExecuteReader();


                if (this._sqlDataReader.HasRows)
                {
                    this._sqlDataReader.Read();
                    result = UserToken.Create(
                        this._sqlDataReader.GetGuid(0),
                        this._sqlDataReader.GetGuid(1),
                        this._sqlDataReader.GetString(2),
                        this._sqlDataReader.GetDateTime(3),
                        this._sqlDataReader.GetDateTime(4));
                }

            }

            return result;


        }
    }
}
