using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Framework.Security2023.Repositories
{
    public class RepositoryUserLoginAttempts
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryUserLoginAttempts()
        {
            this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        public IEnumerable<UserLoginAttempts> GetLoginAttemptsByUserId(Guid userId)
        {
            List<UserLoginAttempts> result = new List<UserLoginAttempts>();
            string sql = "SELECT IdUser, Description, DateCreated FROM UserLoginAttempts where IdUser = @userId;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = this._sqlConnection;
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

        public int SaveLoginAttempt(UserLoginAttempts userLoginAttempt)
        {
            int result;
            string sqlGetUser = "INSERT INTO UserLoginAttempts " +
                "VALUES(@id, @userId, @description, @dateCreated);";
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand();
                this._sqlCommand.Connection = this._sqlConnection;
                this._sqlConnection.Open();
                this._sqlCommand.CommandText = sqlGetUser;
                this._sqlCommand.Parameters.AddWithValue("userId", userLoginAttempt.IdUser);
                this._sqlCommand.Parameters.AddWithValue("description", userLoginAttempt.Description);
                this._sqlCommand.Parameters.AddWithValue("dateCreated", userLoginAttempt.DateCreated);
                result = this._sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

    }
}
