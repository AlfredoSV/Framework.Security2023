using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    internal class RespositoryUser
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        internal RespositoryUser()
        {
            _sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal DtoResponse<bool> ValidateUserByEmail(string email)
        {

            email = string.IsNullOrEmpty(email) ? string.Empty : email;
            bool result = false;
            string procedureName = @"ValidateUser";

            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                _sqlCommand.CommandText = procedureName;
                _sqlCommand.Parameters.AddWithValue("@type", 0);
                _sqlCommand.Parameters.AddWithValue("@value", email);
                _sqlCommand.Parameters.AddWithValue("@result", SqlDbType.Binary);
                _sqlCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                _sqlCommand.ExecuteNonQuery();
                result = _sqlCommand.Parameters["@result"].Value.ToString() == "1";


            }

            return DtoResponse<bool>.Create(result);
        }

        internal DtoResponse<bool> ValidateUserByUserName(string userName)
        {

            userName = string.IsNullOrEmpty(userName) ? string.Empty : userName;
            bool result = false;
            string procedureName = @"ValidateUser";

            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                _sqlCommand.CommandText = procedureName;
                _sqlCommand.Parameters.AddWithValue("@value", userName);
                _sqlCommand.Parameters.AddWithValue("@type", 1);
                _sqlCommand.Parameters.AddWithValue("@result", SqlDbType.Binary);
                _sqlCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                _sqlCommand.ExecuteNonQuery();
                result = _sqlCommand.Parameters["@result"].Value.ToString() == "1";


            }

            return DtoResponse<bool>.Create(result);
        }
        internal DtoResponse<UserFkw> GetUserByUserName(string userName)
        {
            UserFkw userResult = null;

            UserInformation userInformation = null;
            string sqlGetUser = @"Select useFkw.Id, useFkw.UserName, useFkw.Password, 
								useFkw.DateCreated, useFkw.UserCreated, useFkw.LoginSessions, 
								useFkw.UserBlocked, useFkw.ApplyToken, useFkw.RolId,
								useFi.Name, useFi.LastName, useFi.Age, useFi.Address,
								useFi.Email from 
								UserFkw useFkw inner join UserInformation useFi
								on useFkw.Id = useFi.IdUser where 
								useFkw.UserName  = @userName;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("userName", userName);
                _sqlDataReader = _sqlCommand.ExecuteReader();

                if (_sqlDataReader.HasRows)
                {
                    _sqlDataReader.Read();

                    userResult = UserFkw.Create(_sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetString(1), _sqlDataReader.GetString(2),
                        _sqlDataReader.GetDateTime(3), _sqlDataReader.GetGuid(4),
                        _sqlDataReader.GetInt32(5), _sqlDataReader.GetBoolean(6),
                        _sqlDataReader.GetBoolean(7), _sqlDataReader.GetGuid(8));

                    userInformation = UserInformation.Create(_sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetString(9), _sqlDataReader.GetString(10),
                        _sqlDataReader.GetInt32(11), _sqlDataReader.GetDateTime(3),
                        _sqlDataReader.GetString(12), _sqlDataReader.GetString(13),
                        _sqlDataReader.GetGuid(4));

                    userResult.UserInformation = userInformation;

                }

            }

            return DtoResponse<UserFkw>.Create(userResult);
        }

        internal UserFkw GetUser(Guid id)
        {
            UserFkw userResult = null;

            UserInformation userInformation = null;
            string sqlGetUser = @"Select useFkw.Id, useFkw.UserName, useFkw.Password, 
								useFkw.DateCreated, useFkw.UserCreated, useFkw.LoginSessions, 
								useFkw.UserBlocked, useFkw.ApplyToken, useFkw.RolId,
								useFi.Name, useFi.LastName, useFi.Age, useFi.Address,
								useFi.Email from 
								UserFkw useFkw inner join UserInformation useFi
								on useFkw.Id = useFi.IdUser where 
								useFkw.Id  = @id;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("id", id);
                _sqlDataReader = _sqlCommand.ExecuteReader();

                if (_sqlDataReader.HasRows)
                {
                    _sqlDataReader.Read();

                    userResult = UserFkw.Create(_sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetString(1), _sqlDataReader.GetString(2),
                        _sqlDataReader.GetDateTime(3), _sqlDataReader.GetGuid(4),
                        _sqlDataReader.GetInt32(5), _sqlDataReader.GetBoolean(6),
                        _sqlDataReader.GetBoolean(7), _sqlDataReader.GetGuid(8));

                    userInformation = UserInformation.Create(_sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetString(9), _sqlDataReader.GetString(10),
                        _sqlDataReader.GetInt32(11), _sqlDataReader.GetDateTime(3),
                        _sqlDataReader.GetString(12), _sqlDataReader.GetString(13),
                        _sqlDataReader.GetGuid(4));

                    userResult.UserInformation = userInformation;

                }

            }

            return userResult;
        }

        internal bool Save(UserFkw newUser)
        {

            bool result;
            string sqlInsertUser = "INSERT INTO UserFkw VALUES(@id, @userName, @password, @dateCreated, @userCreated, @loginSessions,@rolId, @applyToken, @userBlocked);";
            string sqlInsertUserInformation = "INSERT INTO UserInformation VALUES(@IdUser, @name, @lastName, @age, @dateCreated, @address,@email, @userCreated);";

            SqlTransaction sqlTransaction = null;

            _sqlCommand = new SqlCommand();
            try
            {
                

                _sqlConnection = new SqlConnection(_sqlTextConnection);
                _sqlConnection.Open();
                sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.Transaction = sqlTransaction;

                _sqlCommand.CommandText = sqlInsertUser;
                _sqlCommand.Parameters.AddWithValue("Id", newUser.Id);
                _sqlCommand.Parameters.AddWithValue("userName", newUser.UserName);
                _sqlCommand.Parameters.AddWithValue("password", newUser.Password);
                _sqlCommand.Parameters.AddWithValue("dateCreated", newUser.DateCreated);
                _sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserCreated);
                _sqlCommand.Parameters.AddWithValue("loginSessions", newUser.LoginSessions);
                _sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserBlocked);
                _sqlCommand.Parameters.AddWithValue("rolId", newUser.RolId);
                _sqlCommand.Parameters.AddWithValue("applyToken", newUser.ApplyToken);
                result = _sqlCommand.ExecuteNonQuery() > 0;

                _sqlCommand.Parameters.Clear();

                _sqlCommand.CommandText = sqlInsertUserInformation;
                _sqlCommand.Parameters.AddWithValue("IdUser", newUser.UserInformation.IdUser);
                _sqlCommand.Parameters.AddWithValue("name", newUser.UserInformation.Name);
                _sqlCommand.Parameters.AddWithValue("lastName", newUser.UserInformation.LastName);
                _sqlCommand.Parameters.AddWithValue("dateCreated", newUser.UserInformation.DateCreated);
                _sqlCommand.Parameters.AddWithValue("age", newUser.UserInformation.Age);
                _sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserInformation.UserCreated);
                _sqlCommand.Parameters.AddWithValue("address", newUser.UserInformation.Address);
                _sqlCommand.Parameters.AddWithValue("email", newUser.UserInformation.Email);
                result = result && _sqlCommand.ExecuteNonQuery() > 0;

                sqlTransaction.Commit();

            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();
                throw e;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return result;

        }

        internal bool Delete(Guid userId)
        {

            bool deleteUserFkw, deleteUserInfo, deleteUserTokens;
            _sqlCommand = new SqlCommand();
            SqlTransaction sqlTransaction = null;

            try
            {
                _sqlConnection = new SqlConnection(_sqlTextConnection);


                _sqlConnection.Open();
                sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.Transaction = sqlTransaction;

                _sqlCommand.CommandText = "DELETE FROM UserFkw WHERE Id = @Id";
                _sqlCommand.Parameters.AddWithValue("Id", userId);
                deleteUserFkw = _sqlCommand.ExecuteNonQuery() > 0;


                _sqlCommand.CommandText = "DELETE FROM UserInformation WHERE idUser = @Id";
                deleteUserInfo = _sqlCommand.ExecuteNonQuery() > 0;


                _sqlCommand.CommandText = "DELETE FROM UserToken WHERE id = @Id";
                deleteUserTokens = _sqlCommand.ExecuteNonQuery() > 0;

                sqlTransaction.Commit();


            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();

                throw e;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return deleteUserFkw || deleteUserInfo || deleteUserTokens;

        }

        internal int UpdatePassword(Guid userId, string newPassword)
        {

            int result;
            string sqlGetUser = "UPDATE UserFkw SET Password = @password WHERE Id = @id";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("id", userId);
                _sqlCommand.Parameters.AddWithValue("password", newPassword);
                result = _sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

        internal int Update(UserFkw newUser)
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
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("Id", newUser.Id);
                _sqlCommand.Parameters.AddWithValue("userName", newUser.UserName);
                _sqlCommand.Parameters.AddWithValue("password", newUser.Password);
                _sqlCommand.Parameters.AddWithValue("dateCreated", newUser.DateCreated);
                _sqlCommand.Parameters.AddWithValue("userCreated", newUser.UserCreated);
                _sqlCommand.Parameters.AddWithValue("loginSessions", newUser.LoginSessions);
                _sqlCommand.Parameters.AddWithValue("userBlocked", newUser.UserBlocked);
                _sqlCommand.Parameters.AddWithValue("rolId", newUser.RolId);
                _sqlCommand.Parameters.AddWithValue("applyToken", newUser.ApplyToken);
                result = _sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

        internal int UpdateStatusBlocked(Guid userId, bool status)
        {

            int result;
            string sqlGetUser = @"
             UPDATE dbo.UserFkw
             SET 
	         UserBlocked = @userBlocked
             WHERE Id = @Id;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("Id", userId);
                _sqlCommand.Parameters.AddWithValue("userBlocked", status);
                result = _sqlCommand.ExecuteNonQuery();

            }

            return result;

        }

        internal void UpdateLoginSession(Guid userId, int session)
        {

            int result;
            string sqlGetUser = @"
             UPDATE dbo.UserFkw
             SET 
	         LoginSessions = @session
             WHERE Id = @Id;";
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.Connection = _sqlConnection;
                _sqlConnection.Open();
                _sqlCommand.CommandText = sqlGetUser;
                _sqlCommand.Parameters.AddWithValue("Id", userId);
                _sqlCommand.Parameters.AddWithValue("session", session);
                result = _sqlCommand.ExecuteNonQuery();

            }

        }


    }
}
