using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    public class RepositoryPermission
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryPermission()
        {
            this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        public IEnumerable<Permission> GetPermission(Guid idRole)
        {

            string sql = "SELECT PermissionName, Id, RolId, PermissionDescription, Module FROM Permission WHERE RolId = @rolId;";

            List<Permission> permissions = new List<Permission>();
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand(sql, this._sqlConnection);
                this._sqlConnection.Open();
                this._sqlCommand.Parameters.AddWithValue("rolId", idRole);
                this._sqlDataReader = this._sqlCommand.ExecuteReader();

                if (this._sqlDataReader.HasRows)
                {
                    while (this._sqlDataReader.Read())
                    {
                        this._sqlDataReader.Read();
                        permissions.Add(Permission.Create(
                            this._sqlDataReader.GetString(0),
                            this._sqlDataReader.GetGuid(1),
                            this._sqlDataReader.GetGuid(2),
                            this._sqlDataReader.GetString(3),
                            this._sqlDataReader.GetString(4))
                            );

                    }
                    
                }

            }

            return permissions;
        }

    }
}
