using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    internal class RepositoryToken
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryToken()
        {
            _sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal int Save(UserToken userToken)
        {
            int result;
            string sqlGetUser = "INSERT INTO UserToken " +
                "VALUES(@id, @userId, @token, @dateCreated, @dateExpiration);";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("id", userToken.Id);
                _sqlCommand.Parameters.AddWithValue("userId", userToken.UserId);
                _sqlCommand.Parameters.AddWithValue("token", userToken.Token);
                _sqlCommand.Parameters.AddWithValue("dateCreated", userToken.DateCreated);
                _sqlCommand.Parameters.AddWithValue("dateExpiration", userToken.DateExpiration);
                result = _sqlCommand.ExecuteNonQuery();

            }

            return result;


        }

        internal UserToken GetLastToken(Guid id)
        {
            UserToken result = null;
            string sqlGetUser = @"Select Id,UserId, Token, DateCreated,
            DateExpiration from userToken where
            DateCreated = (Select MAX(DateCreated)
            from UserToken
            where userid = @userId)
            and userid = @userId;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("userId", id);
                _sqlDataReader = _sqlCommand.ExecuteReader();


                if (_sqlDataReader.HasRows)
                {
                    _sqlDataReader.Read();
                    result = UserToken.Create(
                        _sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetGuid(1),
                        _sqlDataReader.GetString(2),
                        _sqlDataReader.GetDateTime(3),
                        _sqlDataReader.GetDateTime(4));
                }

            }

            return result;


        }
    }
}
