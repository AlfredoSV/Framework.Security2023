using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    class RepositoryChangePasswordRequest
    {
        private readonly string _sqlStr;

        internal RepositoryChangePasswordRequest()
        {
            _sqlStr = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal void Save(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                string insertStr = @"INSERT INTO ChangePasswordRequest VALUES(newId(),
                 @userId,@dateExpired,@dateCreated)";

                using (SqlConnection sqlConnection = new SqlConnection(_sqlStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = sqlConnection.CreateCommand();
                    cmd.CommandText = insertStr;
                    cmd.Parameters.AddWithValue("userId", changePasswordRequest.UserId);
                    cmd.Parameters.AddWithValue("dateExpired", changePasswordRequest.DateExpired);
                    cmd.Parameters.AddWithValue("dateCreated", changePasswordRequest.DateCreated);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        internal ChangePasswordRequest SelectByIdRequest(Guid idRequest)
        {
            string insertStr = @"Select * from ChangePasswordRequest where IdRequest = @idRequest";
            ChangePasswordRequest changePasswordRequest = null;
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(_sqlStr))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = insertStr;
                cmd.Parameters.AddWithValue("idRequest", idRequest);

                sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();

                    changePasswordRequest = ChangePasswordRequest.Create(
                        sqlDataReader.GetGuid(0), sqlDataReader.GetGuid(1),
                        sqlDataReader.GetDateTime(2), sqlDataReader.GetDateTime(3));

                }

            }

            return changePasswordRequest;


        }
    }
}
