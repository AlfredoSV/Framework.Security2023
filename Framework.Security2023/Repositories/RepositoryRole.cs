using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    internal class RepositoryRole
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryRole(){
            _sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal Role GetRoleByUserId(Guid userId)
        {
            string sql = @"SELECT ro.Id, ro.RolName, ro.DateCreated, ro.UserCreated ,ro.Active from Rol ro inner join UserFkw
                        us on ro.Id = us.RolId where us.Id = @userId";

            Role role = null;
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand(sql,_sqlConnection);
                _sqlConnection.Open();
                _sqlCommand.Parameters.AddWithValue("userId", userId);
                _sqlDataReader = _sqlCommand.ExecuteReader();

                if (_sqlDataReader.HasRows)
                {
                    _sqlDataReader.Read();
                    role = Role.Create(
                        _sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetString(1),
                        _sqlDataReader.GetDateTime(2),
                        _sqlDataReader.GetGuid(3),
                        _sqlDataReader.GetBoolean(4)
                        );
                }

            }

            return role;
        }

        internal Role GetRoleById(Guid roleId)
        {
            string sql = @"SELECT ro.Id, ro.RolName, ro.DateCreated, ro.UserCreated ,ro.Active from Rol ro where ro.Id = @roleId";

            Role role = null;
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand(sql, _sqlConnection);
                _sqlConnection.Open();
                _sqlCommand.Parameters.AddWithValue("roleId", roleId);
                _sqlDataReader = _sqlCommand.ExecuteReader();

                if (_sqlDataReader.HasRows)
                {
                    _sqlDataReader.Read();
                    role = Role.Create(
                        _sqlDataReader.GetGuid(0),
                        _sqlDataReader.GetString(1),
                        _sqlDataReader.GetDateTime(2),
                        _sqlDataReader.GetGuid(3),
                        _sqlDataReader.GetBoolean(4)
                        );
                }

            }

            return role;
        }

        internal bool InsertRole(Role rol)
        {
            string sql = @"INSERT INTO Rol Values(@Id,@RolName,@DateCreated,@UserCreated,@Active);";

            _sqlCommand = new SqlCommand();

            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand(sql, _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("@Id" , rol.Id);
                _sqlCommand.Parameters.AddWithValue("@RolName", rol.RolName);
                _sqlCommand.Parameters.AddWithValue("@DateCreated", rol.DateCreated);
                _sqlCommand.Parameters.AddWithValue("@UserCreated", rol.UserCreated);
                _sqlCommand.Parameters.AddWithValue("@Active", rol.Active);               
                return _sqlCommand.ExecuteNonQuery() == 1;
            }
        }


    }
}
