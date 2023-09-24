using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    public class RepositoryRole
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryRole(){
            this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        public Role GetRoleByUserId(Guid userId)
        {
            string sql = @"SELECT ro.Id, ro.RolName, ro.DateCreated, ro.UserCreated ,ro.Active from Rol ro inner join UserFkw
                        us on ro.Id = us.RolId where us.Id = @userId";

            Role role = null;
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand(sql,this._sqlConnection);
                this._sqlConnection.Open();
                this._sqlCommand.Parameters.AddWithValue("userId", userId);
                this._sqlDataReader = this._sqlCommand.ExecuteReader();

                if (this._sqlDataReader.HasRows)
                {
                    this._sqlDataReader.Read();
                    role = Role.Create(
                        this._sqlDataReader.GetGuid(0),
                        this._sqlDataReader.GetString(1),
                        this._sqlDataReader.GetDateTime(2),
                        this._sqlDataReader.GetGuid(3),
                        this._sqlDataReader.GetBoolean(4)
                        );
                }

            }

            return role;
        }

        public Role GetRoleById(Guid roleId)
        {
            string sql = @"SELECT ro.Id, ro.RolName, ro.DateCreated, ro.UserCreated ,ro.Active from Rol ro where ro.Id = @roleId";

            Role role = null;
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand(sql, this._sqlConnection);
                this._sqlConnection.Open();
                this._sqlCommand.Parameters.AddWithValue("roleId", roleId);
                this._sqlDataReader = this._sqlCommand.ExecuteReader();

                if (this._sqlDataReader.HasRows)
                {
                    this._sqlDataReader.Read();
                    role = Role.Create(
                        this._sqlDataReader.GetGuid(0),
                        this._sqlDataReader.GetString(1),
                        this._sqlDataReader.GetDateTime(2),
                        this._sqlDataReader.GetGuid(3),
                        this._sqlDataReader.GetBoolean(4)
                        );
                }

            }

            return role;
        }


    }
}
