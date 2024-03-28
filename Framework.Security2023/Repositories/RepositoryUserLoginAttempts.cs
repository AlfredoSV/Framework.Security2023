using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Framework.Security2023.Repositories
{
    internal class RepositoryUserLoginAttempts
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        internal RepositoryUserLoginAttempts()
        {
            _sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal IEnumerable<UserLoginAttempts> GetLoginAttemptsByUserId(Guid userId)
        {
            List<UserLoginAttempts> result = new List<UserLoginAttempts>();
            string sql = "SELECT IdUser, Description, DateCreated FROM UserLoginAttempts where IdUser = @userId;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sql;
                _sqlCommand.Parameters.AddWithValue("userId", userId);
                _sqlDataReader = _sqlCommand.ExecuteReader();

                if (_sqlDataReader.HasRows)
                {
                    while (_sqlDataReader.Read())
                    {
                        result.Add(UserLoginAttempts.Create(_sqlDataReader.GetGuid(0),
                            _sqlDataReader.GetString(1)));
                    }
                }

            }

            return result;

        }

        internal int SaveLoginAttempt(UserLoginAttempts userLoginAttempt)
        {
            int result;
            string sqlGetUser = "INSERT INTO UserLoginAttempts " +
                "VALUES(@userId, @description, @dateCreated);";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("userId", userLoginAttempt.IdUser);
                _sqlCommand.Parameters.AddWithValue("description", userLoginAttempt.Description);
                _sqlCommand.Parameters.AddWithValue("dateCreated", userLoginAttempt.DateCreated);
                result = _sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

    }
}
